using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Entities
{
    public class Trainer : BaseEntity
    {
        public string Name { get; set; }  
        public int TrainerLevel { get; set; }
        public ICollection<PokemonTrainer> Pokemons { get; set; } = new List<PokemonTrainer>(); // Navigation property to the Pokemons owned by the trainer
    }
}
