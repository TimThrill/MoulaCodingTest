using FluentValidation;
using Moula.Payment.GateWay.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moula.Payment.GateWay.Application.Validations
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(c => c.Amount).GreaterThanOrEqualTo(0).LessThanOrEqualTo(Decimal.MaxValue).WithMessage("Payment amount must be larger than zero.");
            RuleFor(c => c.CreatedDate).GreaterThanOrEqualTo(DateTimeOffset.UtcNow.Date).WithMessage("Payment created date must be behind current time.");
        }
    }
}
