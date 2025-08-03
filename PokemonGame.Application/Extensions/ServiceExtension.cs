using Microsoft.Extensions.DependencyInjection;
using PokemonGame.Application.Service;
using PokemonGame.Contracts.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services)
        {
           services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
            services.AddScoped<IPokemonService, PokemonService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<ITrainerService, TrainerService>();
            services.AddScoped<ITrainerPokemonService, TrainerPokemonService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IGymService, GymService>();
            services.AddScoped<IWildPokemonService, WildPokemonService>();
            services.AddScoped<IBattleService, BattleService>();
            services.AddScoped<IWildBattleService, WildBattleService>();
            return services;
        }
    }
}
