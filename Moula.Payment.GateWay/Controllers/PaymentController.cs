using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moula.Payment.GateWay.Application.Commands;
using Moula.Payment.GateWay.Application.ViewModels;

namespace Moula.Payment.GateWay.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPaymentQuery _paymentQuery;


        public PaymentController(IMediator mediator,
            IPaymentQuery paymentQuery)
        {
            _mediator = mediator;
            _paymentQuery = paymentQuery;
        }

        [HttpGet]
        public IActionResult HealthCheck()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentAsync([FromBody] CreatePaymentCommand payment)
        {
            await _mediator.Send(payment);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CancelPaymentAsync([FromBody] Guid paymentId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPaymentAsync([FromBody] Guid paymentId)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetBalanceAndPaymentsAsync(int? userId)
        {
            if(userId == null)
            {
                return NotFound();
            }
            var result = await _paymentQuery.GetUserBalanceAndPaymentsAsync(userId.Value);
            return Ok(result);
        }
    }
}
