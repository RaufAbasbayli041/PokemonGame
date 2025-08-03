using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Entities
{
    public class WildBattle : BaseEntity
    {
        public int WildPokemonId { get; set; } // Foreign key to the WildPokemon entity
        public WildPokemon WildPokemon { get; set; } // Navigation property to the WildPokemon entity
        public int TrainerPokemonId { get; set; } // Foreign key to the Trainer entity
        public TrainerPokemon Trainer { get; set; } // Navigation property to the Trainer entity
        public DateTime BattleDate { get; set; } = DateTime.UtcNow; // Default to current date and time
        
        public int? TrainerPokemonWinnerId { get; set; } // Foreign key to the TrainerPokemon entity for the looser 
        public int? TrainerPokemonLooserId { get; set; } // Foreign key to the TrainerPokemon entity for the looser

        public int LocationId { get; set; } // Foreign key to the Location entity
        public Location Location { get; set; } // Navigation property to the Location entity
        public string? Result { get; set; } // Result of the battle (e.g., "Win", "Loss", "Draw")
    }
}
