using Bug.API.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Bug.Data.Infrastructure;

namespace Bug.API.ActionFilter
{
    public class RoleFilter : ActionFilterAttribute
    {
        public string Permission { get; set; }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.HttpContext.Request.Query["token"].ToString();
            var sv = context.HttpContext.RequestServices;
            var jwtUtils = sv.GetService<IJwtUtils>();
            var uow = sv.GetService<IUnitOfWork>();
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
                    var user = uow.Account.GetByIdAsync(result.Id);
                }
            }
        }
    }
}
