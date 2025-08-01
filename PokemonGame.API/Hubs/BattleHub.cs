using Microsoft.AspNetCore.SignalR;
using PokemonGame.Contracts.Dtos;

namespace PokemonGame.API.Hubs
{
    public class BattleHub : Hub
    {
        public async Task SendTurn (BattleTurnDto turn)
        {
            // Broadcast the turn to all connected clients
            await Clients.All.SendAsync("ReceiveTurn", turn);
        }

        public async Task SendBattleEnd(int winnerId, int loserId)
        {
            // Notify all clients about the end of the battle
            await Clients.All.SendAsync("BattleEnded", new { WinnerId = winnerId, LoserId = loserId });
        }
        public async Task SendAttack(int attackerId, int defenderId, int damage)
        {
            Console.WriteLine($"SendAttack called: {attackerId} -> {defenderId}, damage: {damage}");

            await Clients.All.SendAsync("ReceiveAttack", attackerId, defenderId, damage);
        }
    }
}
