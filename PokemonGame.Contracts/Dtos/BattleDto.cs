using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Dtos
{
    public record BattleDto : BaseDto
    {
        public int Trainer1Id { get; set; } // ID of the first trainer
        public string? Trainer1Name { get; set; } // Name of the first trainer for display purposes
        public int Trainer2Id { get; set; } // ID of the second trainer
        public string? Trainer2Name { get; set; } // Name of the second trainer for display purposes
        public DateTime BattleDate { get; set; } = DateTime.UtcNow; // Date and time of the battle
        public int WinnerId { get; set; } // ID of the winning trainer, if any
        public string? WinnerName { get; set; } // Name of the winning trainer for display purposes
        public int LoserId { get; set; } // ID of the losing trainer, if any
        public string? LoserName { get; set; } // Name of the losing trainer for display purposes
        public int GymId { get; set; } // ID of the Gym where the battle took place
        public string? GymName { get; set; } // Name of the Gym for display purposes

    }
}
