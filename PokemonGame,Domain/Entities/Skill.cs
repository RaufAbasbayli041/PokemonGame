using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Entities
{
    public class Skill : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Power { get; set; } // Power of the skill
        public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
    }

}
