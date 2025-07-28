using PokemonGame.Contracts.Dtos;
using PokemonGame_Domain.Entities;
using PokemonGame_Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Contracts
{
    public interface IBattleService : IGenericService<Battle,BattleDto>
    {
        Task AddTurnAsync(int battleId, int attackerId, int defenderId, BattleAction action, int turnNumber);

    }
}
