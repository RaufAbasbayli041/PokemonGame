 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Entities
{
    public class TrainerPokemon : BaseEntity
    {
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } // Navigation property to Trainer
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; } // Navigation property to Pokemon 
        public DateTime CaughtAt { get; set; } // Date when the Pokemon was caught by the trainer
        public int Level { get; set; } // Level of the Pokemon for this trainer
        public int CurrentHP { get; set; } // Current HP of the Pokemon for this trainer
       
    }
}
