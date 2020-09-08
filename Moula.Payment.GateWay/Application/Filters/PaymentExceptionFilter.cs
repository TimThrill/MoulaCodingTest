using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Moula.Payment.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Moula.Payment.GateWay.Application.Filters
{
    public class PaymentExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is PaymentDomainException exception)
            {
                context.Result = new ObjectResult(exception.InnerException.Message) 
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                context.ExceptionHandled = true;
                
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
