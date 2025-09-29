using Domain.Constants;
using Domain.Exceptions.Extend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            context.ExceptionHandled = true;
            _logger.LogError("ERROR", $"MESSAGE: {exception.Message}");

            string traceId = Activity.Current.Context.TraceId.ToString();

            switch (exception)
            {
                case BadRequestException badRequestValidation:
                    context.Result = new BadRequestObjectResult(new { ErrorCode = badRequestValidation.Code, Message = badRequestValidation.Message, TraceId = traceId });
                    context.ExceptionHandled = true;
                    break;
                case NotFoundException notFoundValidation:
                    context.Result = new NotFoundObjectResult(new { ErrorCode = notFoundValidation.Code, Message = notFoundValidation.Message, TraceId = traceId });
                    context.ExceptionHandled = true;
                    break;
                case ValidateException fluentValidation:
                    context.Result = new BadRequestObjectResult(new { ErrorCode = fluentValidation.Code, Message = fluentValidation.Message, Payloads = fluentValidation.Payloads, TraceId = traceId });
                    context.ExceptionHandled = true;
                    break;
                case PermisionException permisionException:
                    context.Result = new BadRequestObjectResult(new { ErrorCode = permisionException.Code, Message = permisionException.Message, Payloads = permisionException.Payloads, TraceId = traceId });
                    context.ExceptionHandled = true;
                    break;
                case UnauthorizedAccessException _:
                    context.Result = new BadRequestObjectResult(new { ErrorCode = ErrorCodeConstant.UN_AUTHORIZED, Message = MessageErrorConstant.AUTHORIZED, TraceId = traceId });
                    context.ExceptionHandled = true;
                    break;
                default:
                    context.Result = new BadRequestObjectResult(new { ErrorCode = ErrorCodeConstant.UNKNOWN_ERROR, Message = context.Exception.Message, TraceId = traceId });
                    break;
            }
        }
    }
}
