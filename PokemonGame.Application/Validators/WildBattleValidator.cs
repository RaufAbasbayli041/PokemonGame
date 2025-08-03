using FluentValidation;
using PokemonGame.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Validators
{
    public class WildBattleValidator :AbstractValidator<WildBattleDto>
    {
        public WildBattleValidator()
        {
            RuleFor(b => b.TrainerPokemonId)
                .NotEmpty().WithMessage("Trainer Pokemon ID is required.")
                .GreaterThan(0).WithMessage("Trainer Pokemon ID must be greater than 0.");
            RuleFor(b => b.WildPokemonId)
                .NotEmpty().WithMessage("Wild Pokemon ID is required.")
                .GreaterThan(0).WithMessage("Wild Pokemon ID must be greater than 0.");
            RuleFor(b => b.BattleDate)
                .NotEmpty().WithMessage("Battle date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Battle date cannot be in the future.");
            RuleFor(b => b.LocationId)
                .NotEmpty().WithMessage("Location ID is required.")
                .GreaterThan(0).WithMessage("Location ID must be greater than 0.");
            RuleFor(b => b.WinnerId)
                .GreaterThanOrEqualTo(0).WithMessage("Winner ID must be greater than or equal to 0.")
                .When(b => b.WinnerId.HasValue, ApplyConditionTo.CurrentValidator);
            RuleFor(b => b.LoserId)
                .GreaterThanOrEqualTo(0).WithMessage("Loser ID must be greater than or equal to 0.")
                .When(b => b.LoserId.HasValue, ApplyConditionTo.CurrentValidator);

        }
    }
     
   
}
