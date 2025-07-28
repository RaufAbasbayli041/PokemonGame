using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PokemonGame.Application.Validators;
using PokemonGame.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Extensions
{
    public static class ValidatorsExtension
    {
        public static IServiceCollection AddValidatorsRegistration(this IServiceCollection services)
        {

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<PokemonValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<SkillValidator>();
            services.AddValidatorsFromAssemblyContaining<TrainerValidation>();
            services.AddValidatorsFromAssemblyContaining<TrainerPokemonValidator>();
            services.AddValidatorsFromAssemblyContaining<LocationValidator>();
            services.AddValidatorsFromAssemblyContaining<GymValidator>();
            services.AddValidatorsFromAssemblyContaining<WildPokemonValidator>();
            services.AddValidatorsFromAssemblyContaining<BattleValidator>();


            return services;
        }
    }
}
