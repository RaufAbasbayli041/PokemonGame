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
    public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100); // Set maximum length for Name
            builder.HasMany(t => t.TrainerPokemon)
                .WithOne(p => p.Trainer)
                .HasForeignKey(p => p.TrainerId)
                .OnDelete(DeleteBehavior.NoAction); // Set up foreign key relationship with Pokemon
           


        }
    }
}
