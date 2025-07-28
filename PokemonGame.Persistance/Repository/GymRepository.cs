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
    public class GymRepository : GenericRepository<Gym>, IGymRepository
    {
        public GymRepository(PokemonGameDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Gym>> GetAllAsync()
        {
            var datas = await _context.Gyms
                .Include(x => x.LeaderTrainerPokemon)
                .Include(x => x.Location)
                .Include(x => x.Battles)
                .Where(c => !c.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
            return datas;
        }
    }
}
