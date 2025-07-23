using PokemonGame_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Dtos
{
    public record PokemonDto : BaseDto
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Level { get; set; }
        public string? ImageUrl { get; set; } // URL for the Pokemon's image
        public List<int> CategoriesIds { get; set; } = new List<int>(); // Navigation property to Categories
        public ICollection<int> Skills { get; set; } = new List<int>(); // Navigation property to Skills


    }
}
