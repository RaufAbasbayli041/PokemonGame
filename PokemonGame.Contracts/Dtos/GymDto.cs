using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Dtos
{
    public record  GymDto : BaseDto
    {
        public string Name { get; set; } // Name of the Gym
        public string Description { get; set; } // Description of the Gym
        public int LocationId { get; set; } // ID of the Location where the Gym is located
        public string? Location { get; set; } // Location of the Gym
        public int LeaderId { get; set; } // ID of the Gym Leader
        public string? LeaderName { get; set; } // Name of the Gym Leader
        public ICollection<BattleDto> Battles { get; set; } = new List<BattleDto>(); // List of battles associated with the Gym

    }
}
