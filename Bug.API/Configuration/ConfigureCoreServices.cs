using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Data.Infrastructure;
using Bug.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IIssueLogRepo, IssueLogRepo>();
            services.AddScoped<ILabelRepo, LabelRepo>();
            services.AddScoped<IPermissionRepo, PermissionRepo>();
            services.AddScoped<IPriorityRepo, PriorityRepo>();
            services.AddScoped<IProjectRepo, ProjectRepo>();
            services.AddScoped<IRelationRepo, RelationRepo>();
            services.AddScoped<IRoleRepo, RoleRepo>();
            services.AddScoped<IStatusRepo, StatusRepo>();
            services.AddScoped<ITagRepo, TagRepo>();
            services.AddScoped<ITransitionRepo, TransitionRepo>();
            services.AddScoped<IVoteRepo, VoteRepo>();
            services.AddScoped<IWatcherRepo, WatcherRepo>();
            services.AddScoped<IWorkflowRepo, WorkflowRepo>();
            services.AddScoped<IWorklogRepo, WorklogRepo>();

            return services;
        }
    }
}
