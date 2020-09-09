using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moula.Payment.GateWay.Application.Commands
{
    public class CancelPaymentCommand: IRequest
    {
        public Guid PaymentId { get; set; }
        public string Reason { get; set; }
    }
}
