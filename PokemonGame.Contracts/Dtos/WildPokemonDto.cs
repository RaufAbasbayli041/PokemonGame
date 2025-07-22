using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Dtos
{
    public record  WildPokemonDto : BaseDto
    {
        public int PokemonId { get; set; } // ID of the Pokemon
        public string? PokemonName { get; set; } // Name of the Pokemon for display purposes
        public int Level { get; set; } // Level of the wild Pokemon
        public int CurrentHP { get; set; } // Current HP of the wild Pokemon
        public string? ImageUrl { get; set; } // URL for the Pokemon's image
        public DateTime AppearedAt { get; set; } = DateTime.Now;
        public int LocationId { get; set; } // ID of the location where the wild Pokemon appeared
        public string? LocationName { get; set; } // Name of the location for display purposes

    }
    
}
