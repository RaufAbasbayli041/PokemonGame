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
        public int LeaderId { get; set; } // ID of the Gym Leader 
        public List<int> BattlesIds { get; set; } = new List<int>(); // List of battles associated with the Gym

    }
}
