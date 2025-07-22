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
            // Configure the relationship with Trainer

        }
    }
}
