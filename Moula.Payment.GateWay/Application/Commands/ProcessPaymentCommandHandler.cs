using MediatR;
using Moula.Payment.Domain;
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
    public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand>
    {
        private readonly IPaymentRepository _paymentRepository;

        public ProcessPaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Unit> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetPayment(request.PaymentId);

            if(payment == null)
            {
                throw new PaymentDomainException($"Payment id: {request.PaymentId} does not exist.");
            }

            if (payment.Status == Domain.PaymentStatus.Processed || payment.Status == Domain.PaymentStatus.Closed)
            {
                throw new PaymentDomainException($"Payment id: {request.PaymentId} is {Enum.GetName(typeof(PaymentStatus), payment.Status)}");
            }

            var userAccount = await _paymentRepository.GetPaymentAccount(payment.Id);

            payment.Status = PaymentStatus.Processed;
            userAccount.Balance -= payment.Amount;

            await _paymentRepository.UnitOfWork.SaveEntitiesAsync();

            return await Task.FromResult(Unit.Value);
        }
    }
}
