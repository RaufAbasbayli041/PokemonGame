using AutoMapper;
using FluentValidation;
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
        public BattleService(IBattleRepository repository, IMapper mapper, BattleValidator validator) : base(repository, mapper, validator)
        {
            _battleRepository = repository;
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
            int damage = 0;
            var randomDamage = new Random();
            switch (action)
            {
                case BattleAction.Attack:
                    // Simple attack logic: attacker deals a random amount of damage
                    damage = randomDamage.Next(5, 15); // Random damage between 5 and 15
                    defender.CurrentHP -= damage;
                    break;
                case BattleAction.Defend:
                    // Defend logic: reduce incoming damage by half

                    var defendDamage = randomDamage.Next(3, 10); // Random damage between 3 and 10
                    damage = defendDamage / 2; // Defender takes half damage
                    defender.CurrentHP -= damage;
                    break;
                    default : break;
            }

            return damage;

        }


        public Task AddTurnAsync(int battleId, int attackerId, int defenderId, BattleAction action, int turnNumber)
        {
            var turn = new BattleTurn
            {
                BattleId = battleId,
                AttackerId = attackerId,
                DefenderId = defenderId,
                BattleAction = action,
                TurnDate = DateTime.UtcNow
            };
            return _battleRepository.AddTurnAsync(turn);
        }
    }
}
