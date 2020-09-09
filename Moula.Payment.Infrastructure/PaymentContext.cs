using Microsoft.EntityFrameworkCore;
using Moula.Payment.Domain;
using Moula.Payment.Domain.AggregatesModel.UserAggerate;
using Moula.Payment.Infrastructure.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moula.Payment.Infrastructure
{
    public class PaymentContext : DbContext, IUnitOfWork
    {
        public DbSet<Domain.AggregatesModel.PaymentAggerate.Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaymentEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new UserAccountEntityConfiguration());
        }

        public async Task SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            await base.SaveChangesAsync();
        }
    }
}
