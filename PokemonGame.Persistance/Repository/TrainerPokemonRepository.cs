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

        //public async Task<Pokemon> GetPokemonByIdAsync(int pokemonId)
        //{
        //    var pokemon = await _context.Pokemons
        //          .Include(p => p.Skills) // Include skills of the Pokemon
        //          .Include(p => p.Categories) // Include categories of the Pokemon
        //          .Where(p => !p.IsDeleted) // Ensure we only get non-deleted Pokemons
        //          .AsNoTracking()
        //          .FirstOrDefaultAsync(p => p.Id == pokemonId && !p.IsDeleted);
        //    if (pokemon == null)
        //    {
        //        throw new ArgumentException("Pokemon not found");
        //    }
        //    return pokemon;
        //}

        //public Task<Trainer> GetTrainerByIdAsync(int trainerId)
        //{
        //    var trainer = _context.Trainers
        //        .AsNoTracking()
        //         .FirstOrDefaultAsync(t => t.Id == trainerId && !t.IsDeleted);

        //    if (trainer == null)
        //    {
        //        throw new ArgumentException("Trainer not found");
        //    }
        //    return trainer;
        //}

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
    }
}
