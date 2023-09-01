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
    public class HistoryMovementsConfig : IEntityTypeConfiguration<HistoryMovements>
    {
        public void Configure(EntityTypeBuilder<HistoryMovements> builder)
        {
            builder.HasKey(p => p.HistoryMovementsID);
            builder.ToTable("HistoryMovements");

        }
    }
}
