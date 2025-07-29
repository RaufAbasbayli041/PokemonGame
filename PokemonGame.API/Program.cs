using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PokemonGame.Application.Extensions;
using PokemonGame.Application.Profiles;
using PokemonGame.Persistance.DB;
using PokemonGame.Persistance.Extensions;
using PokemonGame.Application.Validators;
using PokemonGame.API.Hubs;
using PokemonGame.Contracts.Contracts;

namespace PokemonGame.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<PokemonGameDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddServiceRegistration();
            builder.Services.AddRepositoryRegistration();

            builder.Services.AddAutoMapper(typeof(CustomProfile).Assembly);
            builder.Services.AddValidatorsRegistration();
            builder.Services.AddSignalR();
            builder.Services.AddScoped<IBattleNotifier,BattleNotifier>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization(); 
            app.MapHub<BattleHub>("/battleHub");


            app.MapControllers();

            app.Run();
        }
    }
}
