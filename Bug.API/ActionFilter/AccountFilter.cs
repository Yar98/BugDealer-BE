using Bug.API.Dto;
using Bug.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.ActionFilter
{
    public class AccountFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync
            (ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if(context
                .ActionArguments
                .SingleOrDefault(o => o.Value is AccountBtsLoginDto)
                .Value is AccountBtsLoginDto login)
            {
                if (!StringHandler.ValidUserName(login.UserName))
                {
                    context.Result = new BadRequestObjectResult("Not valid username");
                    return;
                }
                if (!StringHandler.ValidPassword(login.Password))
                {
                    context.Result = new BadRequestObjectResult("Not valid password");
                    return;
                }
            }
            
            if (context
                .ActionArguments
                .SingleOrDefault(o => o.Value is AccountBtsRegister)
                .Value is AccountBtsRegister register)
            {
                if (!StringHandler.ValidUserName(register.UserName))
                {
                    context.Result = new BadRequestObjectResult("Not valid username");
                    return;
                }
                if (!StringHandler.ValidPassword(register.Password))
                {
                    context.Result = new BadRequestObjectResult("Not valid password");
                    return;
                }
                if (!StringHandler.ValidName(register.FirstName))
                {
                    context.Result = new BadRequestObjectResult("Not valid firstname");
                    return;
                }
                if (!StringHandler.ValidName(register.LastName))
                {
                    context.Result = new BadRequestObjectResult("Not valid lastname");
                    return;
                }
            }

            await next();

        }
    }
}
