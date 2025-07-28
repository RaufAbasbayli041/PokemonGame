using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Entities
{
    public class WildPokemon : BaseEntity
    {
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public DateTime AppearedAt { get; set; } = DateTime.Now;
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public ICollection<WildBattle> WildBattles { get; set; } = new List<WildBattle>(); // Navigation property to the WildBattles involving this WildPokemon

    }
}
