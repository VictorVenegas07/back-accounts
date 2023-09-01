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
    public class HistoryAccountsConfig : IEntityTypeConfiguration<HistoryAccounts>
    {
        public void Configure(EntityTypeBuilder<HistoryAccounts> builder)
        {
            builder.HasKey(p => p.HistoryAccountsID);
            builder.ToTable("HistoryAccounts");
        }
    }
}
