using FluentValidation;
using PokemonGame.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Validators
{
    public class PokemonValidator : AbstractValidator<PokemonDto>
    {
        public PokemonValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");
            RuleFor(p => p.HP)
                .GreaterThan(0).WithMessage("HP must be greater than 0.");
            RuleFor(p => p.Level).NotEmpty()
                .WithMessage("Level is required.")
                .GreaterThanOrEqualTo(1).WithMessage("Level must be at least 1.");




        }
    }
}
