using Microsoft.EntityFrameworkCore;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Persistance.DB;
using PokemonGame_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Persistance.Repository
{
    public class WildBatleRepository : GenericRepository<WildBattle>, IWildBattleRepository
    {
        public WildBatleRepository(PokemonGameDbContext context) : base(context)
        {
        }
        public async Task<BattleTurn> AddTurnAsync(BattleTurn turn)
        {
            var data = await _context.BattleTurns.AddAsync(turn);
            await _context.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<IEnumerable<WildBattle>> GetWildBattlesAsync()
        {
            var datas = await _context.WildBattles
                 .Include(b => b.Trainer)
                     .ThenInclude(tp => tp.Pokemon)
                 .Include(b => b.WildPokemon)
                     .ThenInclude(tp => tp.Pokemon)
                 .Include(b => b.Location)
                 .Where(b => !b.IsDeleted)
                 .AsNoTracking()
                 .ToListAsync();
            return datas;
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

        public async Task<WildBattle> GeWildBattleByIdAsync(int id)
        {
            var wildBattle = await _context.WildBattles
               .Include(b => b.Trainer)
                   .ThenInclude(tp => tp.Pokemon)
               .Include(b => b.WildPokemon)
                   .ThenInclude(tp => tp.Pokemon)
               .Include(b => b.Location)
               .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
            if (wildBattle == null)
            {
                return null;
            }
            return wildBattle;
        }

        public async Task<WildPokemon> GetWildPokemonByIdAsync(int id)
        {
           var wildPokemon = await _context.WildPokemons
                .Include(wp => wp.Pokemon)
                .ThenInclude(p => p.Categories)
                .Include(wp => wp.Pokemon)
                .ThenInclude(p => p.Skills)
                .Include(wp => wp.Pokemon)
                .ThenInclude(p => p.PokemonBaseStats)
                .FirstOrDefaultAsync(wp => wp.Id == id && !wp.IsDeleted);
            if (wildPokemon == null)
            {
                return null;
            }
            return wildPokemon;
        }
    }
}
