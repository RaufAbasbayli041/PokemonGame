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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(PokemonGameDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Category>> GetAllAsync()
        {
           var datas = await _context.Categories
                .Include(x=>x.Pokemons)
                .Where(c => !c.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
            return datas;
        }

        public async Task<List<Pokemon>> GetPokemonsByIds(List<int> ints)
        {
            var datas = await _context.Pokemons
                .Where(c => ints.Contains(c.Id) && !c.IsDeleted)
                .ToListAsync();
            return datas;
        }
    }
}
