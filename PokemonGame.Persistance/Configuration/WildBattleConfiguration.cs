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
    public class WildBattleConfiguration : IEntityTypeConfiguration<WildBattle>
    {
        public void Configure(EntityTypeBuilder<WildBattle> builder)
        {
            builder.Property(wb => wb.BattleDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()"); // Default to current date and time
            builder.HasOne(wb => wb.WildPokemon)
                .WithMany()
                .HasForeignKey(wb => wb.WildPokemonId)
                .OnDelete(DeleteBehavior.NoAction); // Set up foreign key relationship with WildPokemon
           
            builder.HasOne(wb => wb.Location)
                .WithMany(l => l.WildBattles)
                .HasForeignKey(wb => wb.LocationId)
                .OnDelete(DeleteBehavior.NoAction); // Set up foreign key relationship with Location
            builder.HasOne(wb => wb.Trainer)
                .WithMany()
                .HasForeignKey(wb => wb.TrainerPokemonId)
                .OnDelete(DeleteBehavior.NoAction); // Set up foreign key relationship with Trainer



        }
    }
}
