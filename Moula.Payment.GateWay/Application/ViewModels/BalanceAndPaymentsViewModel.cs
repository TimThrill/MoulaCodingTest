using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moula.Payment.GateWay.Application.ViewModels
{
    public class BalanceAndPaymentsViewModel
    {
        public decimal Balance { get; set; }
        public ICollection<PaymentViewModel> Payments { get; set; }
    }

    public class PaymentViewModel
    {
        public DateTimeOffset CreatedDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
