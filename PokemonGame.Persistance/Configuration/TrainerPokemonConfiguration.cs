using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGame_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Persistance.Configuration
{
    public class TrainerPokemonConfiguration : IEntityTypeConfiguration<TrainerPokemon>
    {
        public void Configure(EntityTypeBuilder<TrainerPokemon> builder)
        {
            builder.HasKey(tp => tp.Id); 
            builder.Property(tp => tp.CaughtAt)
                .IsRequired(); // CaughtAt must be required
            builder.Property(tp => tp.Level)
                .IsRequired()
                .HasDefaultValue(1); // Level must be required and has a default value of 1
            builder.Property(tp => tp.CurrentHP)
                .IsRequired();               
            // Configure relationships
            builder.HasOne(tp => tp.Trainer)
                .WithMany(t => t.Pokemons)
                .HasForeignKey(tp => tp.TrainerId);
            builder.HasOne(tp => tp.Pokemon)
                .WithMany()
                .HasForeignKey(tp => tp.PokemonId);
            // Configure navigation properties
            

        }
    }
}
