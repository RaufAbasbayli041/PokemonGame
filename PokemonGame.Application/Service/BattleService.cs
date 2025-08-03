using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.SignalR;

using PokemonGame.Application.Validators;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Contracts.Dtos;
using PokemonGame_Domain.Entities;
using PokemonGame_Domain.Enum;
using PokemonGame_Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Service
{
    public class BattleService : GenericService<Battle, BattleDto>, IBattleService
    {
        private readonly IBattleRepository _battleRepository;
        private readonly IBattleNotifier _notifier;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ITrainerPokemonRepository _trainerPokemonRepository; 
        public BattleService(IBattleRepository repository, IMapper mapper, BattleValidator validator, IBattleNotifier notifier, IPokemonRepository pokemonRepository, ITrainerPokemonRepository trainerPokemonRepository) : base(repository, mapper, validator)
        {
            _battleRepository = repository;
            _notifier = notifier;
            _pokemonRepository = pokemonRepository;
            _trainerPokemonRepository = trainerPokemonRepository;
        }
        public override async Task<BattleDto> AddAsync(BattleDto dto)
        {
            var p1 = await _battleRepository.GetTrainerByIdAsync(dto.TrainerPokemon1Id);
            var p2 = await _battleRepository.GetTrainerByIdAsync(dto.TrainerPokemon2Id);

            if (p1 == null || p2 == null)
            {
                throw new ArgumentException("One or both trainers do not exist.");
            }
            if (dto.TrainerPokemon1Id == dto.TrainerPokemon2Id)
            {
                throw new ArgumentException("A trainer cannot battle themselves.");
            }
            if (p1.Pokemon.Skills == null || p2.Pokemon.Skills == null || !p1.Pokemon.Skills.Any() || !p2.Pokemon.Skills.Any())
            {
                throw new InvalidOperationException("Both Pokémon must have skills to battle.");
            }

            var battle = new Battle
            {
                TrainerPokemon1Id = dto.TrainerPokemon1Id,
                TrainerPokemon2Id = dto.TrainerPokemon2Id,
                BattleDate = dto.BattleDate,
                GymId = dto.GymId
            };

            var createdBattle = await _battleRepository.AddAsync(battle);
            await BattleAsync(createdBattle, p1, p2);
            await _battleRepository.UpdateAsync(createdBattle);
            return _mapper.Map<BattleDto>(createdBattle);
        }
        private async Task BattleAsync(Battle battle, TrainerPokemon p1, TrainerPokemon p2 )
        {
            int turnNumber = 1;
            bool p1Turn = true;
          
            while (p1.CurrentHP > 0 && p2.CurrentHP > 0)
            {
                TrainerPokemon attacker = p1Turn ? p1 : p2;
                TrainerPokemon defender = p1Turn ? p2 : p1;

                var actions = Enum.GetValues(typeof(BattleAction)).Cast<BattleAction>().ToList();
                var random = new Random();
                var action = actions[random.Next(actions.Count)];

                int damage = await PerformAction(attacker, defender, action);

                await AddTurnAsync(battle.Id, attacker.Id, defender.Id, action, turnNumber);

                if (defender.CurrentHP <= 0)
                {
                    battle.TrainerPokemonWinnerId = attacker.Id;
                    battle.TrainerPokemonLoserId = defender.Id;
                     


                    await _trainerPokemonRepository.TransferPokemon(defender.PokemonId, attacker.TrainerId);

                    // Обновляем статистику побед и поражений
                    attacker.Wins++;
                    defender.Losses++;
                    await _trainerPokemonRepository.UpdateAsync(attacker);
                    await _trainerPokemonRepository.UpdateAsync(defender);

                    // Обновляем текущие HP
                    p1.CurrentHP = p1.Pokemon.HP;
                    p2.CurrentHP = p2.Pokemon.HP;
                    await _trainerPokemonRepository.UpdateAsync(p1);
                    await _trainerPokemonRepository.UpdateAsync(p2);

                    break;
                }

                p1Turn = !p1Turn;
                turnNumber++;
            }
        }
        private async Task<int> PerformAction(TrainerPokemon attacker, TrainerPokemon defender, BattleAction action)
        {
            var attackerPokemon = await _pokemonRepository.GetPokemonWithStatsAsync(attacker.PokemonId);
            var defenderPokemon = await _pokemonRepository.GetPokemonWithStatsAsync(defender.PokemonId);

            // Защита — отдельная логика
            if (action == BattleAction.Defend)
            {
                return 0; // ничего не происходит
            }

            // Атака — выбираем случайный скилл
            var skill = attackerPokemon.Skills
                .OrderBy(_ => Guid.NewGuid())
                .FirstOrDefault();

            if (skill == null)
            {
                throw new InvalidOperationException($"Attacker {attackerPokemon.Name} has no skills.");
            }

            int damage = CalculateDamage(attackerPokemon, defenderPokemon, skill);
            defenderPokemon.HP -= damage;
            if (defenderPokemon.HP < 0) defenderPokemon.HP = 0;

            // Обновляем текущие HP
            defender.CurrentHP = defenderPokemon.HP;
            attacker.CurrentHP = attackerPokemon.HP;
            
            return damage;
        }

        public int CalculateDamage(Pokemon attacker, Pokemon defender, Skill skill)
        {
            int attack = attacker.PokemonBaseStats.Attack;
            int defense = defender.PokemonBaseStats.Defense;
            int level = attacker.Level;
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill), "Skill cannot be null.");
            }
            if (attack <= 0 || defense <= 0)
            {
                throw new ArgumentException("Attack and defense values must be greater than zero.");
            }
            if (level <= 0)
            {
                throw new ArgumentException("Level must be greater than zero.");
            }
            if (skill.Power <= 0)
            {
                throw new ArgumentException("Skill power must be greater than zero.");
            }
            // Calculate damage using the formula
            int power = skill.Power;

            double baseDamage = (((2 * level / 5.0 + 2) * attack * power / defense) / 50.0) + 2;

            return (int)Math.Floor(baseDamage);
        }


        public async Task AddTurnAsync(int battleId, int attackerId, int defenderId, BattleAction action, int turnNumber)
        {
            var turn = new BattleTurn
            {
                BattleId = battleId,
                BattleAction = action,
                AttackerId = attackerId,
                DefenderId = defenderId,
                TurnNumber = turnNumber,
                TurnDate = DateTime.UtcNow
            };
            await _battleRepository.AddTurnAsync(turn);
            await _notifier.NotifyTurnAsync(battleId, attackerId, defenderId, action.ToString(), turnNumber);

        }

        public async Task<IEnumerable<BattleDto>> GetBattlesAsync()
        {
            var battles = await _battleRepository.GetBattlesAsync();
            return _mapper.Map<IEnumerable<BattleDto>>(battles);
        }

        public override async Task<BattleDto> GetByIdAsync(int id)
        {
            var battle = await _battleRepository.GetBattleByIdAsync(id);
            if (battle == null)
            {
                return null;
            }
            return _mapper.Map<BattleDto>(battle);
        }
    }
}
