using Bug.API.Dto;
using Bug.API.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Bug.API.ActionFilter
{
    public class ProjectFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync
            (ActionExecutingContext context, 
            ActionExecutionDelegate next)
        {
            var sv = context.HttpContext.RequestServices;
            var projectService = sv.GetService<IProjectService>();
            if (context
                .ActionArguments
                .SingleOrDefault(o => o.Value is ProjectPostDto)
                .Value is ProjectPostDto pro)
            {
                if (await projectService.GetProjectsByCodeCreatorId(pro.CreatorId,pro.Code) != null)
                {
                    context.Result = new BadRequestObjectResult("Code is exist");
                    return;
                }
            }
                await next();
        }
    }
}
