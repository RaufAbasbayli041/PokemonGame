using FluentValidation;
using PokemonGame.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Validators
{
    public class TrainerValidation : AbstractValidator<TrainerDto>
    {
        public TrainerValidation() { 
            RuleFor(trainer => trainer.Name)
                .NotEmpty().WithMessage("Trainer name is required.")
                .Length(2, 50).WithMessage("Trainer name must be between 2 and 50 characters.");
        }
    }
}
