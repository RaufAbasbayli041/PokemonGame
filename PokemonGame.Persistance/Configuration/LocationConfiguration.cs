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
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(100); // Set maximum length for Name
            builder.Property(l => l.Description)
                .HasMaxLength(500); // Set maximum length for Description
            // Configure the one-to-many relationship with Gym
            builder.HasMany(l => l.Gyms)
                .WithOne(g => g.Location)
                .HasForeignKey(g => g.LocationId)
                .OnDelete(DeleteBehavior.NoAction); // Set up foreign key relationship with Gym
            // Configure the one-to-many relationship with WildPokemon
            builder.HasMany(l => l.WildPokemons)
                .WithOne(wp => wp.Location)
                .HasForeignKey(wp => wp.LocationId)
                .OnDelete(DeleteBehavior.NoAction); // Set up foreign key relationship with WildPokemon
            // Configure the one-to-many relationship with WildBattle
            builder.HasMany(l => l.WildBattles)
                .WithOne(wb => wb.Location)
                .HasForeignKey(wb => wb.LocationId)
                .OnDelete(DeleteBehavior.NoAction); // Set up foreign key relationship with WildBattle


        }
    }
}
