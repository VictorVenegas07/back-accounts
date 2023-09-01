using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class MovementBalanceConfig : IEntityTypeConfiguration<MovementBalance>
    {
        public void Configure(EntityTypeBuilder<MovementBalance> builder)
        {
            builder.HasKey(p => p.IdMovementBalance);
            builder.ToTable("MovementBalance");

            builder
                .HasOne<HistoryMovements>(h => h.HistoryMovements)
                .WithMany(m => m.MovementBalances)
                .HasForeignKey(m => m.HistoryMovementsID);
        }
    }
}
