using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonGame.Persistance.DB;
using PokemonGame_Domain.Entities;
using PokemonGame_Domain.Repository;

namespace PokemonGame.Persistance.Repository
{
    public class WildPokemonRepository : GenericRepository<WildPokemon>, IWildPokemonRepository
    {
        public WildPokemonRepository(PokemonGameDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<WildPokemon>> GetAllAsync()
        {
            var datas = await _context.WildPokemons
                .Include(x => x.Location)
                .Include(t => t.WildBattles)
                .Where(w => !w.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
            return datas;

        }
        public override async Task<WildPokemon> AddAsync(WildPokemon entity)
        {
            var pokemon = await _context.Pokemons
                .FirstOrDefaultAsync(p => p.Id == entity.PokemonId && !p.IsDeleted && p.IsWild);
            if (pokemon == null)
            {
                throw new ArgumentException("Pokemon not found or is deleted.");
            }
            
            entity.Pokemon = pokemon;
            var addedEntity = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return addedEntity.Entity;
        }
    }
}
