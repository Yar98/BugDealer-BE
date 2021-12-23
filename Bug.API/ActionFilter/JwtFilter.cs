using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Bug.API.Utils;
using Bug.Data.Infrastructure;
using Bug.Core.Common;
using Bug.API.Services;

namespace Bug.API.ActionFilter
{
    public class JwtFilter : ActionFilterAttribute
    {
        public int Permission { get; set; }
        public string ProjectId { get; set; }

        public override async Task OnActionExecutionAsync
            (ActionExecutingContext context,
            ActionExecutionDelegate next)
        {

            // logic before action goes here
            var param = context.HttpContext.Request.Headers["token"].ToString();
            var sv = context.HttpContext.RequestServices;
            var jwtUtils = sv.GetService<IJwtUtils>();
            var accountService = sv.GetService<IAccountService>();
            var issueService = sv.GetService<IIssueService>();
            var projectService = sv.GetService<IProjectService>();
            if (string.IsNullOrEmpty(param))
            {
                context.Result = new BadRequestObjectResult("Token not found");
                return;
            }

            var result = jwtUtils.ValidateToken(param);
            var user = await accountService
                .GetDetailAccountById(result.Id);
            if (result == null)
            {
                context.Result = new UnauthorizedObjectResult("Invalid token");
                return;
            }

            var projectId = context.RouteData.Values["projectId"]?.ToString();
            // check follow project
            if (!string.IsNullOrEmpty(projectId) &&
                !user.AccountProjectRoles.Any(apr => apr.ProjectId == projectId))
            {
                context.Result = new BadRequestObjectResult("You not join this project");
                return;
            }
            var issueId = context.RouteData.Values["issueId"]?.ToString();
            if (!string.IsNullOrEmpty(issueId))
            {
                var issue = await issueService
                    .GetDetailIssueAsync(issueId);
                if (!user.AccountProjectRoles.Any(apr => apr.ProjectId == issue.ProjectId))
                {
                    context.Result = new BadRequestObjectResult("You not join this issue");
                    return;
                }
            }

            switch (Permission)
            {
                case Bts.GetDetailProject:
                    context.Result = new BadRequestObjectResult("Permission not allow");
                    return;
                case 2://edit issue
                    
                    break;
                default:
                    break;
            }


            await next(); // the actual action

            // logic after the action goes here
        }
    }
}
