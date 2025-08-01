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
    public class TrainerRepository : GenericRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(PokemonGameDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Trainer>> GetAllAsync()
        {
            var datas = await _context.Trainers
               .Include(t => t.TrainerPokemon)
               .ThenInclude(tp => tp.Pokemon)
                 .Where(t => !t.IsDeleted)
                 .AsNoTracking()
                 .ToListAsync();
            return datas;
        }

        public async Task<List<TrainerPokemon>> GetPokemonByTrainerIdAsync(int trainerId)
        {
            var data = await _context.TrainerPokemons
                .Include(tp => tp.Pokemon)
                .Where(tp => tp.TrainerId == trainerId && !tp.IsDeleted)
                .ToListAsync();
            if (data == null || !data.Any())
            {
                throw new KeyNotFoundException($"TrainerPokemon with TrainerId {trainerId} not found.");
            }
            return data;
        }

        public override async Task<Trainer> GetByIdAsync(int id)
        {
            var data = await _context.Trainers
                .Include(t => t.TrainerPokemon)
                .ThenInclude(tp => tp.Pokemon)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
            if (data == null)
            {
                return null;
            }
            return data;
        }
    }
}
