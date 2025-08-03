using PokemonGame_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Repository
{
    public interface IBattleRepository : IGenericRepository<Battle>
    {
        Task<TrainerPokemon> GetTrainerByIdAsync(int id);
        Task<BattleTurn> AddTurnAsync(BattleTurn turn);
        Task<IEnumerable<Battle>> GetBattlesAsync();
        Task<Battle> GetBattleByIdAsync(int id);

	}
}
