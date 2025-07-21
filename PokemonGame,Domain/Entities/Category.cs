using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        // Navigation property to the Pokemons in this category
        public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
    }
}
