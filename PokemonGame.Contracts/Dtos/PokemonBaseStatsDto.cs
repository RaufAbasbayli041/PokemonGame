using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Dtos
{
    public record PokemonBaseStatsDto : BaseDto
    {
        public int Attack { get; set; } // Base Attack stat
        public int Defense { get; set; } // Base Defense stat       
        public int Speed { get; set; } // Base Speed stat
        //public int SpecialAttack { get; set; } // Base Special Attack stat
        //public int SpecialDefense { get; set; } // Base Special Defense stat

        // Navigation property to the Pokemon entity
        public int PokemonId { get; set; } // Foreign key to the Pokemon entity
    }
}
