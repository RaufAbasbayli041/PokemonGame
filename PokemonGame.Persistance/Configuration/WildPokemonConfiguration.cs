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
    public class WildPokemonConfiguration : IEntityTypeConfiguration<WildPokemon>
    {
        public void Configure(EntityTypeBuilder<WildPokemon> builder)
        {
          
            builder.Property(wp => wp.Level)
                .IsRequired(); // Level must be required
            builder.Property(wp => wp.HP)
                .IsRequired();
            builder.HasOne(wp => wp.Location)
                .WithMany(l => l.WildPokemons)
                .HasForeignKey(wp => wp.LocationId)
                .OnDelete(DeleteBehavior.NoAction); // Set up foreign key relationship with Location
            builder.HasOne(wp => wp.Pokemon)
                .WithOne()
                .HasForeignKey<WildPokemon>(wp => wp.PokemonId)
                .OnDelete(DeleteBehavior.NoAction); // Set up foreign key relationship with Pokemon
            builder.Property(wp=>wp.AppearedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()"); // Default value for when the wild Pokemon appeared

            


        }
    }
}
