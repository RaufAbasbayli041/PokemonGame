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
    public class PokemonRepository : GenericRepository<Pokemon>, IPokemonRepository
    {
        public PokemonRepository(PokemonGameDbContext context) : base(context)
        {
        }


        public async Task<List<Category>> GetCategoriesByIdsAsync(List<int> categoryIds)
        {
            var datas = await _context.Categories
                   .Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted)
                   .ToListAsync();
            return datas;
        }

        public async Task<Pokemon> UploadImgAsyn(int id, string imagePath)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon == null)
            {
                throw new ArgumentException("Pokemon not found");
            }
            pokemon.ImageUrl = imagePath;

            _context.Update(pokemon);
            await _context.SaveChangesAsync();
            return pokemon;
        }

        public override async Task<IEnumerable<Pokemon>> GetAllAsync()
        {
            var data = await _context.Pokemons
                .Include(c => c.Categories)
                .Include(c => c.Skills)
                .Include(c=>c.PokemonBaseStats)
                .Where(c => !c.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
            return data;
        }

        public async Task<List<Skill>> GetSkillByIdsAsync(List<int> skillIds)
        {
            var datas = await _context.Skills.Where(c => skillIds.Contains(c.Id) && !c.IsDeleted).ToListAsync();
            return datas;
        }

        public override async Task<Pokemon> GetByIdAsync(int id)
        {
            var pokemon = await _context.Pokemons
                .Include(c => c.Categories)
                .Include(c => c.Skills)
                .Include(c => c.PokemonBaseStats)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
            return pokemon;
        }

        public async Task<Pokemon> GetPokemonWithStatsAsync(int id)
        {
            var pokemon = await _context.Pokemons
                .Include(c => c.PokemonBaseStats)
                .Include(c => c.Categories)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted); 
            return pokemon;
        }

        
    }

}
