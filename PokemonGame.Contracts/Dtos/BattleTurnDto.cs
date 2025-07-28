using PokemonGame_Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Dtos
{
    public record BattleTurnDto : BaseDto
    {
        public int BattleId { get; set; } // Foreign key to the Battle entity
        public int AttackerId { get; set; } // ID of the attacking Pokemon
        public int DefenderId { get; set; } // ID of the defending Pokemon
        public BattleAction BattleAction { get; set; } // Enum representing the action taken in this turn
        public int Damage { get; set; } // Amount of damage dealt in this turn
        public int TurnNumber { get; set; } // The turn number in the battle sequence, useful for tracking the order of turns

        public DateTime TurnDate { get; set; } = DateTime.UtcNow; // Date and time when the turn occurred, default to current date and time

    }
}
