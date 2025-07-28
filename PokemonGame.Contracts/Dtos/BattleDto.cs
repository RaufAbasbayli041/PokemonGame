using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Dtos
{
    public record BattleDto : BaseDto
    {
        public int TrainerPokemon1Id { get; set; } // ID of the first trainer
        public int TrainerPokemon2Id { get; set; } // ID of the second trainer
        public DateTime BattleDate { get; set; } = DateTime.UtcNow; // Date and time of the battle
        public int? WinnerId { get; set; } // ID of the winning trainer, if any
        public int? LoserId { get; set; } // ID of the losing trainer, if any
        public int GymId { get; set; } // ID of the Gym where the battle took place

    }
}
