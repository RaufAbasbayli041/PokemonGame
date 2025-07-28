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

        public int? TrainerPokemonWinnerId { get; set; } // ID of the winning trainer
        public TrainerPokemon Winner { get; set; } // Navigation property to the winning trainer
        public int? TrainerPokemonLoserId { get; set; } // ID of the losing trainer
        public TrainerPokemon Loser { get; set; } // Navigation property to the losing trainer
        public ICollection<BattleTurn> BattleTurns { get; set; } = new List<BattleTurn>(); // Collection of battle turns

        public int GymId { get; set; }
        public Gym Gym { get; set; }

    }
}
