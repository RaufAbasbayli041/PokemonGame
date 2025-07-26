using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Dtos
{
    public record LocationDto :BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> GymsIds { get; set; } = new List<int>(); // Navigation property to the Gyms in this location
        public List<int> WildPokemonsIds { get; set; } = new List<int>(); // Navigation property to the WildPokemons in this location
        public List<int> WildBattlesIds { get; set; } = new List<int>(); // Navigation property to the WildBattles in this location
    }
}
