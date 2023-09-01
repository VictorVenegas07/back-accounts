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
    public class NaturalConfig : IEntityTypeConfiguration<Natural>
    {
        public void Configure(EntityTypeBuilder<Natural> builder)
        {
            builder.ToTable("Natural");

            builder.HasKey(p => p.IdNatural);

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
            .WithMany(u => u.Naturals)
            .HasForeignKey(a => a.Id);

            builder
             .HasOne<HistoryAccounts>(c => c.HistoryAccounts)
             .WithOne(h => h.Natural)
             .HasForeignKey<HistoryAccounts>(h => h.IdNatural);

            builder
            .HasOne<HistoryMovements>(c => c.HistoryMovements)
            .WithOne(h => h.Natural)
            .HasForeignKey<HistoryMovements>(h => h.IdNatural);
        }
    }
}
