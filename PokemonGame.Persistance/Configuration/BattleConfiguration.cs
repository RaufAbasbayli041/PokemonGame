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
            builder.HasOne(builder => builder.Trainer1)
                .WithMany()
                .HasForeignKey(builder => builder.Trainer1Id)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(builder => builder.Trainer2)
                .WithMany()
                .HasForeignKey(builder => builder.Trainer2Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
