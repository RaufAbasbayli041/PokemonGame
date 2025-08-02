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
    public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
    {
        public void Configure(EntityTypeBuilder<Pokemon> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.HP)
                .IsRequired()
                .HasDefaultValue(100); // Default HP value for a Pokemon
            builder.Property(p => p.Level)
                .IsRequired();
            builder.Property(p => p.ImageUrl)
                .HasMaxLength(500); // Set maximum length for ImageUrl
            // Configure many-to-many relationship with Category
            builder.HasMany(p => p.Categories)
                .WithMany(c => c.Pokemons)
                .UsingEntity(j => j.ToTable("PokemonCategories")); // Join table for many-to-many relationship
            // Configure many-to-many relationship with Skill
            builder.HasMany(p => p.Skills)
                .WithMany(s => s.Pokemons)
                .UsingEntity(j => j.ToTable("PokemonSkills")); // Join table for many-to-many relationship

            builder.HasOne(p => p.PokemonBaseStats)
                     .WithOne(b => b.Pokemon)

                   .HasForeignKey<PokemonBaseStats>(b => b.PokemonId)
                   .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
