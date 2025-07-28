using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Entities
{
    public class Pokemon : BaseEntity
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Level { get; set; }
        public bool IsWild { get; set; } // Indicates if the Pokemon is wild or not
        public string? ImageUrl { get; set; } // URL for the Pokemon's image
        public PokemonBaseStats PokemonBaseStats { get; set; } // Enum representing the base stats of the Pokemon
        public ICollection<Category> Categories { get; set; } = new List<Category>(); // Navigation property to Categories
        public ICollection<Skill> Skills { get; set; } = new List<Skill>(); // Navigation property to Skills
    }
}
