using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moula.Payment.GateWay.Application.Commands
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand>
    {
        public Task<Unit> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}
