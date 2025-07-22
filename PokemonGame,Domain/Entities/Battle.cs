using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Entities
{
    public class Battle : BaseEntity
    {
        public int Trainer1Id { get; set; }
        public Trainer Trainer1 { get; set; }

        public int Trainer2Id { get; set; }
        public Trainer Trainer2 { get; set; }

        public DateTime BattleDate { get; set; } = DateTime.UtcNow; // Default to current date and time

        public int? WinnerId { get; set; } // ID of the winning trainer
        public Trainer Winner { get; set; } // Navigation property to the winning trainer
        public int? LoserId { get; set; } // ID of the losing trainer
        public Trainer Loser { get; set; } // Navigation property to the losing trainer

        public int GymId { get; set; }
        public Gym Gym { get; set; }

    }
}
