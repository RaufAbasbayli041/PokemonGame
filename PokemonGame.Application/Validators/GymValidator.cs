using FluentValidation;
using PokemonGame.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Validators
{
    public class GymValidator : AbstractValidator<GymDto>
    {
        public GymValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Gym name is required.")
                .MaximumLength(100).WithMessage("Gym name must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Gym description is required.")
                .MaximumLength(500).WithMessage("Gym description must not exceed 500 characters.");

        }
    }
}
