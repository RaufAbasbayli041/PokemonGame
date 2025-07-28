using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Entities
{
    public class Gym : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LeaderTrainerPokemonId { get; set; }                  
        public TrainerPokemon LeaderTrainerPokemon { get; set; }
        public int LocationId { get; set; } // Foreign key to the Location entity
        public Location Location { get; set; } // Navigation property to the Location entity
        public ICollection<Battle> Battles { get; set; } = new List<Battle>();


    }
}
