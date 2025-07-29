using FluentValidation;
using PokemonGame.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Validators
{
    public class SkillValidator : AbstractValidator<SkillDto>
    {
        public SkillValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(10, 200).WithMessage("Description must be between 10 and 200 characters.");
            RuleFor(c => c.Power)
                .GreaterThanOrEqualTo(0).WithMessage("Power must be a non-negative integer.")
                .LessThanOrEqualTo(100).WithMessage("Power must not exceed 100.");
        }

    }
}
