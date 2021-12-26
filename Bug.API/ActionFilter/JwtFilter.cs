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
using Bug.API.BtsException;
using Bug.Entities.Model;

namespace Bug.API.ActionFilter
{
    public class JwtFilter : ActionFilterAttribute
    {
        public int Permission { get; set; }

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
            var permissionService = sv.GetService<IPermissionService>();
            if (string.IsNullOrEmpty(param))
            {
                context.Result = new BadRequestObjectResult("Token not found");
                return;
            }

            var result = jwtUtils.ValidateToken(param);
            if (result == null)
            {
                context.Result = new UnauthorizedObjectResult("Invalid token");
                return;
            }

            var user = await accountService
                .GetDetailAccountById(result.Id);
            if(user == null)
            {
                context.Result = new UnauthorizedObjectResult("Invalid token");
                return;
            }

            // check access follow project
            var projectId = context.RouteData.Values["projectId"]?.ToString();
            if (!string.IsNullOrEmpty(projectId) &&
                !user.AccountProjectRoles.Any(apr => apr.ProjectId == projectId))
            {
                throw new NotJoinThisProject();
            }

            // check access to issue
            Issue issue = null;
            var issueId = context.RouteData.Values["issueId"]?.ToString();
            if (!string.IsNullOrEmpty(issueId))
            {
                issue = await issueService
                    .GetDetailIssueAsync(issueId);
                if (!user.AccountProjectRoles.Any(apr => apr.ProjectId == issue.ProjectId))
                {
                    throw new NotJoinThisProject();
                }
            }

            if(Permission != 0)
            {
                var cate = await permissionService.GetPermissionByIdAsync(Permission);
                switch (cate.CategoryId)
                {
                    case (int)Bts.Category.ProjectPermission:
                        var accessProject = await accountService
                            .CheckPermissionsOfRolesOfAccount(user.Id, Permission, projectId);
                        if (accessProject == null)
                            throw new PermissionNotAllowed();
                        break;
                    case (int)Bts.Category.IssuePermission:
                        if(issue == null)
                        {
                            context.Result = new BadRequestObjectResult("issue not found");
                            return;
                        }
                        var accessIssue = await accountService
                            .CheckPermissionsOfRolesOfAccount(user.Id, Permission, issue.ProjectId);
                        if (accessIssue == null &&
                            issue.AssigneeId != user.Id && 
                            issue.ReporterId != user.Id)
                            throw new PermissionNotAllowed();
                        break;
                    default:
                        break;
                }
            }
            


            await next(); // the actual action

            // logic after the action goes here
        }
    }
}
