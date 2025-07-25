﻿using FluentValidation;
using PokemonGame.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Validators
{
    public class TrainerPokemonValidator : AbstractValidator<TrainerPokemonDto>
    {
        public TrainerPokemonValidator() 
        {
            RuleFor(tp => tp.TrainerId)
                .NotEmpty().WithMessage("Trainer ID is required.")
                .GreaterThan(0).WithMessage("Trainer ID must be greater than 0.");
            RuleFor(tp => tp.PokemonId)
                .NotEmpty().WithMessage("Pokemon ID is required.")
                .GreaterThan(0).WithMessage("Pokemon ID must be greater than 0.");
            RuleFor(tp => tp.CaughtAt)
                .NotEmpty().WithMessage("Caught date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Caught date cannot be in the future.");
            RuleFor(tp => tp.Level)
                .NotEmpty().WithMessage("Level is required.")
                .GreaterThan(0).WithMessage("Level must be greater than 0.");
            RuleFor(tp => tp.CurrentHP)
                .NotEmpty().WithMessage("Current HP is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Current HP cannot be negative.");
            
        }
    }
}
