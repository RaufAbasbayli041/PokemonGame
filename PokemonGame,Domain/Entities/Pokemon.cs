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
        public int HP { get; set; } // Current HP of the Pokemon for this trainer
        public int Level { get; set; }
        public string? ImageUrl { get; set; } // URL for the Pokemon's image
        public ICollection<Category> Categories { get; set; } = new List<Category>(); // Navigation property to Categories
        public ICollection<Skill> Skills { get; set; } = new List<Skill>(); // Navigation property to Skills
    }
}
