using Microsoft.EntityFrameworkCore;
using PokemonGame_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Persistance.DB
{
    public class PokemonGameDbContext : DbContext
    {
        public PokemonGameDbContext(DbContextOptions<PokemonGameDbContext> options) : base(options)
        {

        }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<TrainerPokemon> TrainerPokemons { get; set; }
        public DbSet<WildPokemon> WildPokemons { get; set; }
        public DbSet<WildBattle> WildBattles { get; set; }
        public DbSet<BattleTurn> BattleTurns { get; set; }
        public DbSet<PokemonBaseStats> PokemonBaseStats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PokemonGameDbContext).Assembly);
        }

    }
}
