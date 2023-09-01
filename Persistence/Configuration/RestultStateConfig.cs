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
    public class RestultStateConfig : IEntityTypeConfiguration<RestultState>
    {
        public void Configure(EntityTypeBuilder<RestultState> builder)
        {
            builder.ToTable("RestultState");

            builder.HasKey(p => p.IdRestultState);

            builder.Property(p => p.AccountName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Description)
              .HasMaxLength(500);

            builder.Property(p => p.Code)
              .IsRequired();

            builder.Property(p => p.Class)
              .HasMaxLength(50)
              .IsRequired();

            builder.Property(p => p.State)
              .IsRequired();

            builder.Property(p => p.Type)
              .HasMaxLength(50)
              .IsRequired();

            builder.Property(p => p.InitialDate)
              .IsRequired();


            builder
            .HasOne<User>(a => a.User)
            .WithMany(u => u.RestultStates)
            .HasForeignKey(a => a.Id);

            builder
             .HasOne<HistoryAccounts>(c => c.HistoryAccounts)
             .WithOne(h => h.RestultState)
             .HasForeignKey<HistoryAccounts>(h => h.IdRestultState);

            builder
            .HasOne<HistoryMovements>(c => c.HistoryMovements)
            .WithOne(h => h.RestultState)
            .HasForeignKey<HistoryMovements>(h => h.IdRestultState);
        }
    }
}
