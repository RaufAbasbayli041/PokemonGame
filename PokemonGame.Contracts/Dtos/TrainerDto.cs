using PokemonGame_Domain.Entities;
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
        public ICollection<TrainerPokemon> Pokemons { get; set; } = new List<TrainerPokemon>(); // Navigation property to the Pokemons owned by the trainer
    }
    
}
