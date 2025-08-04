using Microsoft.AspNetCore.SignalR;
using PokemonGame.Contracts.Contracts;

namespace PokemonGame.API.Hubs
{
    public class BattleNotifier : IBattleNotifier
    {
        private readonly IHubContext<BattleHub> _hubContext;

        public BattleNotifier(IHubContext<BattleHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task NotifyAttackAsync(object id, int parsedAttackerId, int parsedDefenderId, int damage)
        {
            return _hubContext.Clients.All.SendAsync("ReceiveAttack", new
            {
                BattleId = id,
                AttackerId = parsedAttackerId,
                DefenderId = parsedDefenderId,
                Damage = damage
            });
        }

        public Task NotifyBattleEndAsync(int battleId, int winnerId, int loserId)
        {
            return _hubContext.Clients.All.SendAsync("BattleEnded", new
            {
                BattleId = battleId,
                WinnerId = winnerId,
                LoserId = loserId
            });
        }

        public async Task NotifyTurnAsync(int battleId, int attackerId, int defenderId, string action, int turnNumber)
        {
           await _hubContext.Clients.All.SendAsync("ReceiveTurn", new
           {
               BattleId = battleId,
               AttackerId = attackerId,
               DefenderId = defenderId,
               Action = action,
               TurnNumber = turnNumber
           });
        }
    }
}
