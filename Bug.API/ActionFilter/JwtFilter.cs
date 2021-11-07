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
            var param = context.HttpContext.Request.Query["token"].ToString();
            var sv = context.HttpContext.RequestServices;
            var jwtUtils = sv.GetService<IJwtUtils>();
            var accountService = sv.GetService<IAccountService>();
            if (string.IsNullOrEmpty(param))
            {
                context.Result = new BadRequestObjectResult("Token not found");
                return;
            }
            else
            {
                var result = jwtUtils.ValidateToken(param);
                if (result == null)
                {
                    context.Result = new UnauthorizedObjectResult("Invalid token");
                    return;
                }
                else // success login
                {
                    var user = await accountService.GetAccountByIdWithRolesAsync(result.Id);
                    switch (Permission)
                    {
                        case Bts.GetDetailProject:
                            if (!user.Roles.Any(
                                r =>
                                r.Permissions.Any(p => p.Id == Permission) && 
                                r.Projects.Any(p=>p.Id==ProjectId)))
                            {
                                context.Result = new BadRequestObjectResult("Permission not allow");
                                return;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            await next(); // the actual action

            // logic after the action goes here
        }
    }
}
