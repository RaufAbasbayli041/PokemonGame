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
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(PokemonGameDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Location>> GetAllAsync()
        {
            var datas = await _context.Locations
                .Include(x => x.Gyms).ThenInclude(x => x.LeaderTrainerPokemon)
                .Include(x => x.WildPokemons)
                .Include(x => x.WildBattles)
                .Where(c => !c.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
            return datas;

        }
    }
}
