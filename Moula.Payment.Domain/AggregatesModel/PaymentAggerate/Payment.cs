using System;
using System.Collections.Generic;
using System.Text;

namespace Moula.Payment.Domain.AggregatesModel.PaymentAggerate
{
    public class Payment
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
