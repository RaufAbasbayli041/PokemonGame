using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGame_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Persistance.Configuration
{
    public class BattleTurnConfigurations : IEntityTypeConfiguration<BattleTurn>
    {
        public void Configure(EntityTypeBuilder<BattleTurn> builder)
        {

            builder.HasOne(bt => bt.Attacker)
                    .WithMany()
                    .HasForeignKey(bt => bt.AttackerId)
                    .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(bt => bt.Defender)
                .WithMany()
                .HasForeignKey(bt => bt.DefenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
