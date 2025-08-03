using Microsoft.EntityFrameworkCore;
using PokemonGame.Persistance.DB;
using PokemonGame_Domain.Entities;
using PokemonGame_Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Persistance.Repository
{
    public class TrainerPokemonRepository : GenericRepository<TrainerPokemon>, ITrainerPokemonRepository
    {
        public TrainerPokemonRepository(PokemonGameDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<TrainerPokemon>> GetAllAsync()
        {
            var datas = await _context.TrainerPokemons
                .Include(tp => tp.Trainer)
                .Include(tp => tp.Pokemon)
                .ThenInclude(p => p.Skills) // Include skills of the Pokemon
                .Include(p => p.Pokemon) // Include category of the Pokemon
                .ThenInclude(p => p.Categories) // Include category of the Pokemon
                .Where(tp => !tp.IsDeleted)
                .Include(tp => tp.Pokemon)
                .ThenInclude(p => p.PokemonBaseStats) // Include base stats of the Pokemon
                .AsNoTracking() // Use AsNoTracking for read-only operations
                .ToListAsync();
            return datas;
        }

        public override async Task<TrainerPokemon> AddAsync(TrainerPokemon entity)
        {
            var trainer = await _context.Trainers
                .FirstOrDefaultAsync(t => t.Id == entity.TrainerId && !t.IsDeleted);
            if (trainer == null)
            {
                throw new ArgumentException("Trainer not found or is deleted.");
            }
            var pokemon = await _context.Pokemons
                .FirstOrDefaultAsync(p => p.Id == entity.PokemonId && !p.IsDeleted && !p.IsWild);
            if (pokemon == null)
            {
                throw new ArgumentException("Pokemon not found or is deleted.");
            }
            entity.Trainer = trainer;
            entity.Pokemon = pokemon;
            var addedEntity = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return addedEntity.Entity;
        }

        public async Task TransferPokemon(int pokemonId, int newTrainerId)
        {
            var loserTrainerPokemon = await _context.TrainerPokemons
                .Include(tp => tp.Pokemon)
                .FirstOrDefaultAsync(tp => tp.PokemonId == pokemonId && !tp.IsDeleted);
            if (loserTrainerPokemon == null)
            {
                throw new ArgumentException("Pokemon not found or is deleted.");
            }
            var newTrainer = await _context.Trainers
                .FirstOrDefaultAsync(t => t.Id == newTrainerId && !t.IsDeleted);
            if (newTrainer == null)
            {
                throw new ArgumentException("New trainer not found or is deleted.");
            }
            loserTrainerPokemon.TrainerId = newTrainerId;
            _context.TrainerPokemons.Update(loserTrainerPokemon);
            await _context.SaveChangesAsync();
        }

        public async Task CaughtPokemon(int pokemonId, int trainerId)
        {
            var pokemon = await _context.Pokemons
                .FirstOrDefaultAsync(p => p.Id == pokemonId && !p.IsDeleted && p.IsWild);
            if (pokemon == null)
            {
                throw new ArgumentException("Pokemon not found or is not wild.");
            }
             
            var trainerPokemon = new TrainerPokemon
            {
                TrainerId = trainerId,
                PokemonId = pokemonId,
                CurrentHP = pokemon.HP, 
                IsDeleted = false
            };
            await AddAsync(trainerPokemon);

            pokemon.IsWild = false;
            _context.Pokemons.Update(pokemon);
            await _context.SaveChangesAsync();
        }

    }
}
