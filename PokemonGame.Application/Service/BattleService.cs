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
        public BattleService(IBattleRepository repository, IMapper mapper, BattleValidator validator, IBattleNotifier notifier) : base(repository, mapper, validator)
        {
            _battleRepository = repository;
            _notifier = notifier;
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
            if (dto.WinnerId != 0 && dto.WinnerId != dto.TrainerPokemon1Id && dto.WinnerId != dto.TrainerPokemon2Id)
            {
                throw new ArgumentException("Winner ID must be one of the trainers involved in the battle.");
            }
            if (dto.LoserId != 0 && dto.LoserId != dto.TrainerPokemon1Id && dto.LoserId != dto.TrainerPokemon2Id)
            {
                throw new ArgumentException("Loser ID must be one of the trainers involved in the battle.");
            }

            var battle = new Battle
            {
                TrainerPokemon1Id = dto.TrainerPokemon1Id,
                TrainerPokemon2Id = dto.TrainerPokemon2Id,
                BattleDate = dto.BattleDate,
                GymId = dto.GymId
            };

            var createdBattle = await _battleRepository.AddAsync(battle);

            // Initialize turn number and first turn
            int turnNumber = 1;
            bool p1Turn = true; // true for p1's turn, false for p2's turn

            // Simulate a battle until one Pokemon's HP reaches 0tun
            while (p1.CurrentHP > 0 && p2.CurrentHP > 0)
            {
                TrainerPokemon attacker = p1Turn ? p1 : p2;
                TrainerPokemon defender = p1Turn ? p2 : p1;
                // Randomly select an action for the attacker
                var actions = Enum.GetValues(typeof(BattleAction)).Cast<BattleAction>().ToList();
                var random = new Random();
                var action = actions[random.Next(actions.Count)];

                int damage = PerformAction(attacker, defender, action);

                await AddTurnAsync(createdBattle.Id, attacker.Id, defender.Id, action, turnNumber);

                if (defender.CurrentHP <= 0)
                {
                    createdBattle.TrainerPokemonWinnerId = attacker.Id;
                    createdBattle.TrainerPokemonLoserId = defender.Id;
                    break;
                }

                p1Turn = !p1Turn;
                turnNumber++;

            }
            await _battleRepository.UpdateAsync(createdBattle);
            return _mapper.Map<BattleDto>(createdBattle);
        }
        private int PerformAction(TrainerPokemon attacker, TrainerPokemon defender, BattleAction action)
        {
            var attackerPokemon = attacker.Pokemon;
            var defenderPokemon = defender.Pokemon;

            var skill = attackerPokemon.Skills.FirstOrDefault(s => s.Name == action.ToString());
            if (skill == null)
            {
                throw new ArgumentException($"Skill {action} not found for attacker {attackerPokemon.Name}.");
            }

            int damage = CalculateDamage(attackerPokemon, defenderPokemon, skill);
            defenderPokemon.HP -= damage;
            if (defenderPokemon.HP < 0)
            {
                defenderPokemon.HP = 0; // Ensure HP does not go below zero
            }
            // Update the defender's current HP
            defender.CurrentHP = defenderPokemon.HP;
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
                AttackerId = attackerId,
                DefenderId = defenderId,
                BattleAction = action,
                TurnDate = DateTime.UtcNow
            };
            await _battleRepository.AddTurnAsync(turn);
            await _notifier.NotifyTurnAsync(battleId, attackerId, defenderId, action.ToString(), turnNumber);


        }

    }
}
