using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Entities
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Gym> Gyms { get; set; } = new List<Gym>(); // Navigation property to the Gyms in this location

    }
}
