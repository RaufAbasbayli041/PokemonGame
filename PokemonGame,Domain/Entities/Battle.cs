using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Entities
{
    public class Battle : BaseEntity
    {
        public int TrainerPokemon1Id { get; set; }
        public TrainerPokemon TrainerPokemon1 { get; set; }

        public int TrainerPokemon2Id { get; set; }
        public TrainerPokemon TrainerPokemon2 { get; set; }
        public DateTime BattleDate { get; set; } = DateTime.UtcNow; // Default to current date and time
        public ICollection<BattleTurn> BattleTurns { get; set; } = new List<BattleTurn>(); // Collection of battle turns

        public int GymId { get; set; }
        public Gym Gym { get; set; }

    }
}
