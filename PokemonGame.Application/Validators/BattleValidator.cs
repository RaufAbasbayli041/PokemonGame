using FluentValidation;
using PokemonGame.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Validators
{
    public class BattleValidator : AbstractValidator<BattleDto>
    {
        public BattleValidator()
        {
            RuleFor(b => b.TrainerPokemon1Id)
                .NotEmpty().WithMessage("Trainer 1 ID is required.")
                .GreaterThan(0).WithMessage("Trainer 1 ID must be greater than 0.");
            RuleFor(b => b.TrainerPokemon2Id)
                .NotEmpty().WithMessage("Trainer 2 ID is required.")
                .GreaterThan(0).WithMessage("Trainer 2 ID must be greater than 0.");
            RuleFor(b => b.WinnerId)
                .GreaterThan(0).When(b => b.WinnerId != 0)
                .WithMessage("Winner ID must be greater than 0 if specified.");
            RuleFor(b => b.LoserId)
                .GreaterThan(0).When(b => b.LoserId != 0)
                .WithMessage("Loser ID must be greater than 0 if specified.");
            RuleFor(b => b.GymId)
                .GreaterThan(0).WithMessage("Gym ID must be greater than 0.")
                .When(b => b.GymId != 0)
                .WithMessage("Gym ID is required if the battle took place in a gym.");
            RuleFor(b => b.BattleDate)
                .NotEmpty().WithMessage("Battle date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Battle date cannot be in the future.");

        }
    }
}
