using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentityProvider.Model;
using System.Net;
using Microsoft.Extensions.Logging;
using Bug.API.Dto;
using Bug.Core.Common;
using Bug.API.BtsException;
using Bug.Entities.ModelException;

namespace Bug.API.CustomMiddlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Call the next delegate/middleware in the pipeline
                await _next(context);
            }
            catch (UsernameExistsException e)
            {
                await HandleExceptionAsync(context, e);               
            }
            catch(CannotDeleteDefault e)
            {
                await HandleExceptionAsync(context, e);
            }
            catch(OldPasswordWrong e)
            {
                await HandleExceptionAsync(context, e);
            }
            catch(Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            // custom code for each exception
            string code = exception switch
            {
                UsernameExistsException => "email exsit",
                CannotDeleteDefault => "cannot delete default",
                OldPasswordWrong => "old password wrong",
                DueDateOfIssueMustWithinDueDateOfProject => "due date not valid",
                _ => "undefined"
            };
            context.Response.Headers.Add("error", code);
            _logger.CreateLogger(exception.ToString());
            var result = new ErrorNormalDto
            {
                Status = context.Response.StatusCode,
                Message = exception.Message,
                Error = exception.ToString()
            };
            await context.Response.WriteAsync(Bts.ConvertJson(result));
        }
    }
}
