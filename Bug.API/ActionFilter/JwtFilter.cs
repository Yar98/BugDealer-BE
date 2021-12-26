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
using Bug.API.Dto;

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
                var perm = await permissionService.GetPermissionByIdAsync(Permission);
                switch (perm.CategoryId)
                {
                    case (int)Bts.Category.ProjectPermission:
                        {
                            var accessProject = await accountService
                            .CheckPermissionsOfRolesOfAccount(user.Id, Permission, projectId);
                            if (accessProject == null)
                                throw new PermissionNotAllowed();
                            break;
                        }
                        
                    case (int)Bts.Category.IssuePermission:
                        {
                            
                            if (Permission == (int)Bts.Permission.CloneIssue ||
                                Permission == (int)Bts.Permission.CreateIssue)
                            {
                                var postIssue = context
                                    .ActionArguments
                                    .SingleOrDefault(o => o.Value is IssueNormalDto)
                                    .Value as IssueNormalDto;
                                var accessIssue = await accountService
                                .CheckPermissionsOfRolesOfAccount(user.Id, Permission, postIssue.ProjectId);
                                if (accessIssue == null)
                                    throw new PermissionNotAllowed();
                            }
                            if (Permission != (int)Bts.Permission.CreateIssue &&
                                Permission != (int)Bts.Permission.CloneIssue)
                            {
                                var accessIssue = await accountService
                                .CheckPermissionsOfRolesOfAccount(user.Id, Permission, issue.ProjectId);
                                if (issue == null)
                                {
                                    context.Result = new BadRequestObjectResult("issue not found");
                                    return;
                                }
                                if (accessIssue == null &&
                                    issue.AssigneeId != user.Id &&
                                    issue.ReporterId != user.Id)
                                    throw new PermissionNotAllowed();
                            }
                            break;
                        }                       
                    default:
                        break;
                }
            }
            


            await next(); // the actual action

            // logic after the action goes here
        }

        private void CheckPermission(int permission) { 
        }
    }
}
