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
    public class BattleConfiguration : IEntityTypeConfiguration<Battle>
    {
        public void Configure(EntityTypeBuilder<Battle> builder)
        {
            builder.HasOne(b => b.TrainerPokemon1)
                .WithMany()
                .HasForeignKey(b => b.TrainerPokemon1Id)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(builder => builder.TrainerPokemon2)
                .WithMany()
                .HasForeignKey(builder => builder.TrainerPokemon2Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(builder => builder.Winner)
                .WithMany()
                .HasForeignKey(builder => builder.TrainerPokemonWinnerId)
                .IsRequired(false)

                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(builder => builder.Loser)
                .WithMany()
                .HasForeignKey(builder => builder.TrainerPokemonLoserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(builder => builder.Gym)
                .WithMany()
                .HasForeignKey(builder => builder.GymId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(b => b.BattleDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP") // Set default value to current timestamp
                .ValueGeneratedOnAdd(); // Ensure the value is generated on add



        }
    }
}
