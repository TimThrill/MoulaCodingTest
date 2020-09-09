using MediatR;
using Moula.Payment.Domain.AggregatesModel.PaymentAggerate;
using Moula.Payment.Domain.AggregatesModel.UserAggerate;
using Moula.Payment.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moula.Payment.GateWay.Application.Commands
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPaymentRepository _paymentRepository;

        public CreatePaymentCommandHandler(IUserRepository userRespository,
            IPaymentRepository paymentRepository)
        {
            _userRepository = userRespository;
            _paymentRepository = paymentRepository;
        }

        public async Task<Unit> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var userAccount = await _userRepository.GetUserAccountAsync(request.UserId);

            if (userAccount == null)
            {
                throw new PaymentDomainException($"User id: {request.UserId} does not exist.");
            }

            var payment = new Moula.Payment.Domain.AggregatesModel.PaymentAggerate.Payment
            {
                UserId = userAccount.UserId,
                CreatedDate = request.CreatedDate,
                Status = Domain.PaymentStatus.Pending,
                Amount = request.Amount
            };

            // If account balance is less than the requested payment amount,
            // close the payment with "Not enough funds" message
            if(request.Amount > userAccount.Balance)
            {
                payment.Status = Domain.PaymentStatus.Closed;
                payment.ClosedReason = "Not enough funds";
            }

            await _paymentRepository.AddPayment(payment);

            await _paymentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
