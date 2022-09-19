using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NTQ.Sdk.Core.Filters
{
    public class ErrorHandlingFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ErrorResponse)
            {
                ErrorResponse exception = (ErrorResponse)context.Exception;
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = exception.Error.StatusCode;
                context.Result = new JsonResult(exception.Error);
                return;
            }
#if DEBUG
            context.Result = new ObjectResult(new ErrorResponse()
            {
                Error =
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ErrorCode = 500,
                    Message = context.Exception.StackTrace
                }
            });
#else
            context.Result = new ObjectResult(new ErrorResponse()
            {
                Error =
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ErrorCode = 500,
                    Message = "Opps, something went wrong!"
                }
            });
#endif
        }
    }
}