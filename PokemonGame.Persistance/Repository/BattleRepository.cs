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
    }
}
