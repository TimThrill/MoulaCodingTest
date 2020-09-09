using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moula.Payment.Domain.AggregatesModel.UserAggerate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moula.Payment.Infrastructure.EntityConfigurations
{
    public class UserAccountEntityConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.HasOne<User>().WithOne().HasForeignKey("UserId");
        }
    }
}
