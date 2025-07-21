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

        public DbSet<Gym> Gyms { get; set; }
    }
}
