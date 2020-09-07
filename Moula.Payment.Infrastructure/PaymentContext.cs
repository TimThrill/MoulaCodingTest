using Microsoft.EntityFrameworkCore;
using Moula.Payment.Domain.AggregatesModel.UserAggerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moula.Payment.Infrastructure
{
    public class PaymentContext : DbContext
    {
        public DbSet<Domain.AggregatesModel.PaymentAggerate.Payment> Payments { get;set; }
        public DbSet<User> Users { get; set; }

        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options) { }
    }
}
