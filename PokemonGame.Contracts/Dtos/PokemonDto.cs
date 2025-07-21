using PokemonGame_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Dtos
{
    public record class PokemonDto : BaseDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int Level { get; set; } // The level of the Pokemon
        public int HP { get; set; } // Health Points, using Guid for unique identification
        public CategoryDto Category { get; set; } // Navigation property to Category
        public ICollection<Skill> Skills { get; set; } = new List<Skill>(); // Navigation property to Skills
        public int TrainerId { get; set; }
        public TrainerDto Trainer { get; set; } // Navigation property to Trainer


    }
}
