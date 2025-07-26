using FluentValidation;
using PokemonGame.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Validators
{
    public class LocationValidator :AbstractValidator<LocationDto>
    {
        public LocationValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Location name is required.")
                .MaximumLength(100).WithMessage("Location name must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Location description is required.")
                .MaximumLength(500).WithMessage("Location description must not exceed 500 characters.");
           
        }
    }
}
