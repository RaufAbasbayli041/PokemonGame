using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Contracts
{
    public interface IBattleNotifier
    {
        Task NotifyTurnAsync(int battleId, int attackerId, int defenderId, string action, int turnNumber);
        // This method will be used to notify clients about the battle turn updates.
        Task NotifyBattleEndAsync(int battleId, int winnerId, int loserId);
    }
}
