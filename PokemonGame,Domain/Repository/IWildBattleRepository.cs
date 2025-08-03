using PokemonGame_Domain.Entities;
using PokemonGame_Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Contracts
{
    public interface IWildBattleRepository : IGenericRepository<WildBattle>
    {
        Task<TrainerPokemon> GetTrainerByIdAsync(int id);
        Task<WildPokemon> GetWildPokemonByIdAsync(int id);

        Task<BattleTurn> AddTurnAsync(BattleTurn turn);
        Task<IEnumerable<WildBattle>> GetWildBattlesAsync();
        Task<WildBattle> GeWildBattleByIdAsync(int id);
    }
}
