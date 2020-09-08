using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moula.Payment.GateWay.Application.ViewModels
{
    public interface IPaymentQuery
    {
        Task<BalanceAndPaymentsViewModel> GetUserBalanceAndPaymentsAsync(int userId);
    }
}
