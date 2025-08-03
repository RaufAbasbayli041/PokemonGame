using PokemonGame_Domain.Entities; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Dtos
{
    public record TrainerPokemonDto : BaseDto
    {
        public int TrainerId { get; set; }
        //public string? TrainerName { get; set; } // Name of the Trainer for display purposes        
      //  public int PokemonId { get; set; }
        // public string? PokemonName { get; set; } // Name of the Pokemon for display purposes
        public PokemonDto? Pokemon { get; set; } // Reference to the Pokemon details
        public DateTime CaughtAt { get; set; } // Date when the Pokemon was caught by the trainer
        public int Level { get; set; } // Level of the Pokemon for this trainer 
        public int CurrentHP { get; set; } // Current HP of the Pokemon for this trainer

        public int Losses { get; set; } // Number of losses for this Pokemon in battles
        public int Wins { get; set; } // Number of wins for this Pokemon in battles

    }
}
