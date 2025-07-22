using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Entities
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Gym> Gyms { get; set; } = new List<Gym>(); // Navigation property to the Gyms in this location
        public ICollection<WildPokemon> WildPokemons { get; set; } = new List<WildPokemon>(); // Navigation property to the WildPokemons in this location
        public ICollection<WildBattle> WildBattles { get; set; } = new List<WildBattle>(); // Navigation property to the WildBattles in this location

    }
}
