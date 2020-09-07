using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moula.Payment.GateWay.Application.Commands;

namespace Moula.Payment.GateWay.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult HealthCheck()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand payment)
        {
            await _mediator.Send(payment);
            return Ok();
        }
    }
}
