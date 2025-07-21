using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Entities
{
    public class Battle : BaseEntity
    {
        public int Trainer1Id { get; set; }
        public Trainer Trainer1 { get; set; }

        public int Trainer2Id { get; set; }
        public Trainer Trainer2 { get; set; }

        public int GymId { get; set; }
        public Gym Gym { get; set; }

    }
}
