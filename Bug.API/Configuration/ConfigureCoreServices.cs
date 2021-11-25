using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Bug.Data.Infrastructure;
using Bug.Data.Repositories;
using Bug.API.Services;
using Bug.Entities.Builder;
using Bug.API.Utils;
using Bug.API.ActionFilter;

namespace Bug.API.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<ICommentRepo, CommentRepo>();
            services.AddScoped<IIssueRepo, IssueRepo>();
            services.AddScoped<IIssuelogRepo, IssuelogRepo>();
            services.AddScoped<IPermissionRepo, PermissionRepo>();
            services.AddScoped<IPriorityRepo, PriorityRepo>();
            services.AddScoped<IProjectRepo, ProjectRepo>();
            services.AddScoped<IRelationRepo, RelationRepo>();
            services.AddScoped<IRoleRepo, RoleRepo>();
            services.AddScoped<IStatusRepo, StatusRepo>();
            services.AddScoped<ITagRepo, TagRepo>();
            services.AddScoped<IWorklogRepo, WorklogRepo>();
            services.AddScoped<IFieldRepo, FieldRepo>();
            services.AddScoped<ICustomtypeRepo, CustomtypeRepo>();
            services.AddScoped<IAttachmentRepo, AttachmentRepo>();

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IFieldService, FieldService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IPriorityService, PriorityService>();
            services.AddScoped<IIssuelogService, IssuelogService>();

            services.AddScoped<IProjectBuilder, ProjectBuilder>();
            services.AddScoped<IIssueBuilder, IssueBuilder>();
            services.AddScoped<IAccountBuilder, AccountBuilder>();

            services.AddScoped<IJwtUtils, JwtUtils>();

            services.AddScoped<JwtFilter>();
            services.AddScoped<ModelFilter>();
            services.AddScoped<AccountFilter>();
            return services;
        }
    }
}
