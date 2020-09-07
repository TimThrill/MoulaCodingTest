﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moula.Payment.GateWay.Application.Commands
{
    public class CreatePaymentCommand : IRequest
    {
        public decimal Amount { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
