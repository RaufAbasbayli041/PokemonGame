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
    public class GymConfiguration : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100); // Set maximum length for Name
            builder.Property(g => g.Description)
                .HasMaxLength(500); // Set maximum length for Description
            builder.HasOne(g => g.Leader)
                .WithOne()
                .HasForeignKey<Gym>(g => g.LeaderTrainerPokemonId)
                .OnDelete(DeleteBehavior.NoAction); // Set up foreign key relationship with Trainer
            builder.HasMany(g => g.Battles)
                .WithOne(b => b.Gym)
                .HasForeignKey(b => b.GymId)
                .OnDelete(DeleteBehavior.NoAction); // Set up foreign key relationship with Battle
            builder.HasOne(g => g.Location)
                .WithMany(l => l.Gyms)
                .HasForeignKey(g => g.LocationId)
                .OnDelete(DeleteBehavior.NoAction); // Set up foreign key relationship with Location
        }
    }
}
