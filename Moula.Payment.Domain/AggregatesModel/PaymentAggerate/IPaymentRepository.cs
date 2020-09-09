using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Moula.Payment.Domain.AggregatesModel.PaymentAggerate
{
    public interface IPaymentRepository: IRepository<Payment>
    {
        Task<Payment> AddPayment(Payment payment);
    }
}
