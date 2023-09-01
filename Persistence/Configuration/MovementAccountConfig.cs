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
    public class MovementAccountConfig : IEntityTypeConfiguration<MovementAccount>
    {
        public void Configure(EntityTypeBuilder<MovementAccount> builder)
        {
            builder.HasKey(p => p.IdMovementAccount);
            builder.ToTable("MovementAccount");

            builder
                .HasOne<HistoryAccounts>(h => h.HistoryAccounts)
                .WithMany(m => m.MovementAccounts)
                .HasForeignKey(m => m.HistoryAccountsID);
        }
    }
}
