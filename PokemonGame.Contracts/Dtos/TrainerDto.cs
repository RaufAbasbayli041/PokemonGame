using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Dtos
{
    public record class TrainerDto : BaseDto
    {
        public string Name { get; set; }
        public string BadgeCollection { get; set; } // Collection of badges the trainer has earned
        public ICollection<PokemonDto> Pokemons { get; set; } = new List<PokemonDto>(); // Navigation property to the Pokemons owned by the trainer
    }
    
}
