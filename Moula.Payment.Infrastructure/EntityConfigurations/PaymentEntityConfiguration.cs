using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moula.Payment.Domain.AggregatesModel.UserAggerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moula.Payment.Infrastructure.EntityConfigurations
{
    public class PaymentEntityConfiguration : IEntityTypeConfiguration<Moula.Payment.Domain.AggregatesModel.PaymentAggerate.Payment>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.PaymentAggerate.Payment> builder)
        {
            builder.HasOne<User>().WithMany().HasForeignKey("UserId");
        }
    }
}
