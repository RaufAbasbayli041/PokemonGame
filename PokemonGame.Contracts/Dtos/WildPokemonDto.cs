using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Dtos
{
	public record WildPokemonDto : BaseDto
	{
		public int PokemonId { get; set; } // ID of the Pokemon
		public int Level { get; set; } // Level of the wild Pokemon
		public int CurrentHP { get; set; } // Current HP of the wild Pokemon
		public DateTime AppearedAt { get; set; } = DateTime.UtcNow.AddHours(4); // Timestamp when the wild Pokemon appeared
        public int LocationId { get; set; } // ID of the location where the wild Pokemon appeared

	}

}
