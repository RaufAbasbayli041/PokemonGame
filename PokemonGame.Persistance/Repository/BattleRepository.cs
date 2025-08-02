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
	public class BattleRepository : GenericRepository<Battle>, IBattleRepository
	{
		public BattleRepository(PokemonGameDbContext context) : base(context)
		{
		}

		public async Task<BattleTurn> AddTurnAsync(BattleTurn turn)
		{
			var data = await _context.BattleTurns.AddAsync(turn);
			await _context.SaveChangesAsync();
			return data.Entity;
		}

		public async Task<TrainerPokemon> GetTrainerByIdAsync(int id)
		{
			var trainerPokemon = await _context.TrainerPokemons
				.Include(tp => tp.Pokemon)
				.ThenInclude(p => p.Categories)
				.Include(tp => tp.Pokemon)
				.ThenInclude(p => p.Skills)
				.Include(tp => tp.Pokemon)
				.ThenInclude(p => p.PokemonBaseStats)
				.FirstOrDefaultAsync(tp => tp.Id == id && !tp.IsDeleted);

			if (trainerPokemon == null)
			{
				return null;
			}
			return trainerPokemon;
		}
		public async Task<IEnumerable<Battle>> GetBattlesAsync()
		{
			var data = await _context.Battles
				.Include(b => b.TrainerPokemon1)
					.ThenInclude(tp => tp.Pokemon)
				.Include(b => b.TrainerPokemon2)
					.ThenInclude(tp => tp.Pokemon)
				.Include(b => b.Gym)
				.Where(b => !b.IsDeleted)
				.AsNoTracking()
				.ToListAsync();
			return data;
		}
		public async Task<Battle> GetBattleByIdAsync(int id)
		{
			var battle = await _context.Battles
				.Include(b => b.TrainerPokemon1)
					.ThenInclude(tp => tp.Pokemon)
				.Include(b => b.TrainerPokemon2)
					.ThenInclude(tp => tp.Pokemon)
				.Include(b => b.Gym)
				.AsNoTracking()
				.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
			if (battle == null)
			{
				return null;
			}
			return battle;
		}
	}
}
