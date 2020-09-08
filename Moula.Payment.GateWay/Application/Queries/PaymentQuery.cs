using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moula.Payment.GateWay.Application.ViewModels;
using Moula.Payment.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moula.Payment.GateWay.Application.Queries
{
    public class PaymentQuery : IPaymentQuery
    {
        private readonly PaymentContext _context;
        private readonly IMapper _mapper;

        public PaymentQuery(PaymentContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BalanceAndPaymentsViewModel> GetUserBalanceAndPaymentsAsync(int userId)
        {
            var balanceAndPayment = new BalanceAndPaymentsViewModel();
            balanceAndPayment.Payments = _mapper.Map<ICollection<PaymentViewModel>>(await _context.Payments.Where(p => p.UserId == userId).ToListAsync());
            balanceAndPayment.Balance = _context.UserAccounts.FirstOrDefault(a => a.UserId == userId)?.Balance ?? 0;

            return balanceAndPayment;
        }
    }
}
