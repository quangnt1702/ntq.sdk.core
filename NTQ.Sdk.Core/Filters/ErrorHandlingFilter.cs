﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

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

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                context.Result = new ObjectResult(new ErrorResponse((int)HttpStatusCode.InternalServerError, 500,
                    context.Exception.Message?.ToString()));
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;
            }
            else
            {
                context.Result = new ObjectResult(new ErrorResponse((int)HttpStatusCode.InternalServerError, 500,
                    "Oops! something went wrong!"));
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;
            }
        }
    }
}