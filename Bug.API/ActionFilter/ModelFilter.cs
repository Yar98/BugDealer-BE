using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Microsoft.AspNetCore.Mvc;
using Bug.API.Dto;

namespace Bug.API.ActionFilter
{
    public class ModelFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync
            (ActionExecutingContext context, 
            ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult("Model not valid");
                return;
            }

            await next();

        }
    }
}
