using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
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
    public class WildBattleService : GenericService<WildBattle, WildBattleDto>, IWildBattleService
    {
        private readonly IWildBattleRepository _wildBattleRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ITrainerPokemonRepository _trainerPokemonRepository;
        private readonly IBattleNotifier _notifier;
        private readonly ILogger<WildBattleService> _logger;

        public WildBattleService(IWildBattleRepository repository, IMapper mapper, WildBattleValidator validator, IPokemonRepository pokemonRepository, ITrainerPokemonRepository trainerPokemonRepository, IBattleNotifier notifier, ILogger<WildBattleService> logger) : base(repository, mapper, validator)
        {
            _wildBattleRepository = repository;
            _pokemonRepository = pokemonRepository;
            _trainerPokemonRepository = trainerPokemonRepository;
            _notifier = notifier;
            _logger = logger;
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
            await _wildBattleRepository.AddTurnAsync(turn);
            await _notifier.NotifyTurnAsync(battleId, attackerId, defenderId, action.ToString(), turnNumber);

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

        private async Task WildBattleAsync(WildBattle wildBattle, TrainerPokemon trainerPokemon, WildPokemon wildPokemon)
        {
            _logger.LogInformation($"Wild battle started: {trainerPokemon.Pokemon.Name} vs {wildPokemon.Pokemon.Name} at {wildBattle.BattleDate}");
            int turnNumber = 1;
            bool trainerTurn = true;

            while (trainerPokemon.CurrentHP > 0 && wildPokemon.HP > 0)
            {
                BattleAction action;
                int damage = 0;

                if (trainerTurn)
                {
                    // Trainer attack
                    var actions = Enum.GetValues(typeof(BattleAction)).Cast<BattleAction>().ToList();
                    var random = new Random();
                    action = actions[random.Next(actions.Count)];

                    damage = await PerformAction(trainerPokemon, wildPokemon, action);
                    await AddTurnAsync(wildBattle.Id, trainerPokemon.Id, wildPokemon.Id, action, turnNumber);
                }
                else
                {
                    // Wild Pokemon attack
                    var actions = Enum.GetValues(typeof(BattleAction)).Cast<BattleAction>().ToList();
                    var random = new Random();
                    action = actions[random.Next(actions.Count)];
                    damage = await WildPerformAction(wildPokemon, trainerPokemon, action);
                    await AddTurnAsync(wildBattle.Id, wildPokemon.Id, trainerPokemon.Id, action, turnNumber);

                }

                // Check win
                if (trainerPokemon.CurrentHP <= 0)
                {
                    wildBattle.TrainerPokemonWinnerId = null; // trainer loose
                    wildBattle.TrainerPokemonLooserId = trainerPokemon.Id; // сохраняем покемона в битве
                  trainerPokemon.Losses++; // увеличиваем количество поражений тренера 

                    break;
                }
                if (wildPokemon.HP <= 0)
                {

                    wildBattle.TrainerPokemonWinnerId = trainerPokemon.Id; // trainer win
                    wildBattle.WildPokemonId = wildPokemon.Id; // сохраняем покемона в битве
                    // Перенос покемона в коллекцию тренера
                    wildPokemon.Pokemon.IsWild = false; // Покемон больше не дикий

                    trainerPokemon.Wins++; // увеличиваем количество побед тренера

                    // Пойманный покемон
                    await _trainerPokemonRepository.CaughtPokemon(wildPokemon.PokemonId, trainerPokemon.TrainerId);

                    trainerPokemon.CurrentHP = trainerPokemon.Pokemon.HP;
                    wildPokemon.HP = wildPokemon.Pokemon.HP;
                    await _trainerPokemonRepository.UpdateAsync(trainerPokemon);
                    break;
                }

                trainerTurn = !trainerTurn;
                turnNumber++;
            }

            _logger.LogInformation($"Wild battle ended: {trainerPokemon.Pokemon.Name} vs {wildPokemon.Pokemon.Name} at {wildBattle.BattleDate}");
        }

        // battle action when trainer pokemon attack wild pokemon
        private async Task<int> PerformAction(TrainerPokemon attacker, WildPokemon defender, BattleAction action)
        {
            var attackerPokemon = await _pokemonRepository.GetPokemonWithStatsAsync(attacker.PokemonId);
            var defenderPokemon = await _pokemonRepository.GetPokemonWithStatsAsync(defender.PokemonId);

            // Defender
            if (action == BattleAction.Defend)
            {
                return 0; 
            }
            // Atatck - random skill
            var skill = attackerPokemon.Skills
                .OrderBy(_ => Guid.NewGuid())
                .FirstOrDefault();

            if (skill == null)
            {
                throw new InvalidOperationException($"Attacker {attackerPokemon.Name} has no skills.");
            }

            int damage = CalculateDamage(attackerPokemon, defenderPokemon, skill);
            defenderPokemon.HP -= damage;
            if (defenderPokemon.HP < 0) defender.HP = 0;

            // refresh current HP
            defender.HP = defenderPokemon.HP;
            attacker.CurrentHP = attackerPokemon.HP;

            return damage;
        }

        // battle action when wild pokemin attack trainer pokemon
        private async Task<int> WildPerformAction(WildPokemon attacker, TrainerPokemon defender, BattleAction action)
        {
            var attackerPokemon = await _pokemonRepository.GetPokemonWithStatsAsync(attacker.PokemonId);
            var defenderPokemon = await _pokemonRepository.GetPokemonWithStatsAsync(defender.PokemonId);

            // Defender
            if (action == BattleAction.Defend)
            {
                return 0;  
            }

            // Atatck - random skill
            var skill = attackerPokemon.Skills
                .OrderBy(_ => Guid.NewGuid())
                .FirstOrDefault();

            if (skill == null)
            {
                throw new InvalidOperationException($"Attacker {attackerPokemon.Name} has no skills.");
            }

            int damage = CalculateDamage(attackerPokemon, defenderPokemon, skill);
            defenderPokemon.HP -= damage;
            if (defenderPokemon.HP < 0) defender.CurrentHP = 0;

            // refresh current HP
            defender.CurrentHP = defenderPokemon.HP;
            attacker.HP = attackerPokemon.HP;

            return damage;
        }

        public override async Task<WildBattleDto> AddAsync(WildBattleDto dto)
        {
            var trainerPokemon = await _wildBattleRepository.GetTrainerByIdAsync(dto.TrainerPokemonId);
            var wildPokemon = await _wildBattleRepository.GetWildPokemonByIdAsync(dto.WildPokemonId);

            if (trainerPokemon == null || wildPokemon == null)
            {
                throw new ArgumentException("One or both trainers do not exist.");
            }
          
            if (trainerPokemon.Pokemon.Skills == null || wildPokemon.Pokemon.Skills == null || !trainerPokemon.Pokemon.Skills.Any() || !wildPokemon.Pokemon.Skills.Any())
            {
                throw new InvalidOperationException("Both Pokémon must have skills to battle.");
            }

            var battle = new WildBattle
            {
                TrainerPokemonId = dto.TrainerPokemonId,
                WildPokemonId = dto.WildPokemonId,
                BattleDate = dto.BattleDate,
                LocationId = dto.LocationId,
            };

            var createdBattle = await _wildBattleRepository.AddAsync(battle);
            await WildBattleAsync(createdBattle, trainerPokemon, wildPokemon);
            await _wildBattleRepository.UpdateAsync(createdBattle);
            return _mapper.Map<WildBattleDto>(createdBattle);
        }

        public async Task<IEnumerable<WildBattleDto>> GetBattlesAsync()
        {
            var battles = await _wildBattleRepository.GetWildBattlesAsync();
            if (battles == null || !battles.Any())
            {
                return Enumerable.Empty<WildBattleDto>();
            }
            var battleDtos = _mapper.Map<IEnumerable<WildBattleDto>>(battles);
           
            return battleDtos;

        }
    }
}
