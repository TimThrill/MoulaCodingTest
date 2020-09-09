using Microsoft.EntityFrameworkCore;
using Moula.Payment.Domain;
using Moula.Payment.Domain.AggregatesModel.PaymentAggerate;
using Moula.Payment.Domain.AggregatesModel.UserAggerate;
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

        public async Task<Domain.AggregatesModel.PaymentAggerate.Payment> GetPayment(Guid paymentId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.Id.Equals(paymentId));
        }

        public async Task<UserAccount> GetPaymentAccount(Guid paymentId)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.Id.Equals(paymentId));
            if(payment == null)
            {
                return null;
            }
            return await _context.UserAccounts.FirstOrDefaultAsync(ua => ua.UserId == payment.UserId);
        }
    }
}
