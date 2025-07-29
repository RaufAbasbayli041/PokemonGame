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
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {   builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(s => s.Power)
                .IsRequired()
                .HasDefaultValue(0); // Default power value for skills
            // Configure many-to-many relationship with Pokemon
            builder.HasMany(s => s.Pokemons)
                .WithMany(p => p.Skills)
                .UsingEntity(j => j.ToTable("PokemonSkills")); // Join table for many-to-many relationship
        }
    }
}
