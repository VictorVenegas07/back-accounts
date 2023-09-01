using Aplication.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class AplicationDbContext: IdentityDbContext<User>
    {

        private readonly IDateTimeService _dateTimeService; 
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options, IDateTimeService dateTime):base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTimeService = dateTime;

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTimeService.Now;
                        break;
                    case EntityState.Added:
                        entry.Entity.Created = _dateTimeService.Now;
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
        }
        //public DbSet<User> Users { get; set; }
        public DbSet<HistoryAccounts> HistoryAccounts { get; set; }
        public DbSet<HistoryMovements> HistoryMovements { get; set; }
        public DbSet<MovementAccount> MovementAccounts { get; set; }
        public DbSet<MovementBalance> MovementBalances { get; set; }
        public DbSet<Natural> Naturals { get; set; }
        public DbSet<RestultState> RestultStates { get; set; }


    }
}
