using Moula.Payment.Domain;
using Moula.Payment.Domain.AggregatesModel.PaymentAggerate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moula.Payment.Infrastructure.Repositories
{
    public class PaymentRepository: IPaymentRepository
    {
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        private readonly PaymentContext _context;

        public PaymentRepository(PaymentContext context)
        {
            _context = context;
        }

        public async Task<Domain.AggregatesModel.PaymentAggerate.Payment> AddPayment(Domain.AggregatesModel.PaymentAggerate.Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return payment;
        }
    }
}
