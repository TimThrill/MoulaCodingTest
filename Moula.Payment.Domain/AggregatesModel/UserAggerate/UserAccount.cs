using System;
using System.Collections.Generic;
using System.Text;

namespace Moula.Payment.Domain.AggregatesModel.UserAggerate
{
    public class UserAccount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
