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
    }
}
