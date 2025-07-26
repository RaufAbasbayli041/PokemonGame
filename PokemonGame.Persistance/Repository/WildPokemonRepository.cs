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
				.AsNoTracking()
				.Where(w => !w.IsDeleted)
			    .ToListAsync();
			return datas;

		}
	}
}
