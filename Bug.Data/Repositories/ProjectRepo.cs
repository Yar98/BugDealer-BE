using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Bug.Data.Extensions;
using Bug.Data.Specifications;
using Bug.Core.Utility;

namespace Bug.Data.Repositories
{
    public class ProjectRepo : EntityRepoBase<Project>, IProjectRepo
    {
        public ProjectRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {
            
        }

        public async Task<PaginatedList<Project>> GetPaginatedProjects(string creatorId,
            int pageIndex, int pageSize,
            int categoryId, string tagName,
            string sortOrder,
            CancellationToken cancelltionToken = default)
        {
            var specificationResult =
                new ProjectWithTagsSpecification(creatorId, categoryId, tagName); 
            var result = _bugContext
                .Projects
                .Specify(specificationResult);
            switch (sortOrder)
            {
                case "name":
                    result = result.OrderBy(p => p.Name);
                    break;
                case "startdate":
                    result = result.OrderBy(p => p.StartDate);
                    break;
                case "startdate_desc":
                    result = result.OrderByDescending(p => p.StartDate);
                    break;
                case "enddate":
                    result = result.OrderBy(p => p.EndDate);
                    break;
                case "enddate_desc":
                    result = result.OrderByDescending(p => p.EndDate);
                    break;
                case "recentdate":
                    result = result.OrderBy(p => p.RecentDate);
                    break;
                case "recentdate_desc":
                    result = result.OrderByDescending(p => p.RecentDate);
                    break;
                default:
                    result = result.OrderBy(p => p.Id);
                    break;
            }
            return await PaginatedList<Project>
                .CreateListAsync(result.AsNoTracking(),pageIndex,pageSize,cancelltionToken);
        }
        public async Task<IReadOnlyList<Project>> GetRecentProjects(string accountId,
            int categoryId, 
            string tagName, 
            int count,
            CancellationToken cancelltionToken = default)
        {
            var specificationResult =
                new ProjectWithTagsSpecification(accountId,categoryId,tagName);
            return await _bugContext
                .Projects
                .Specify(specificationResult)
                .OrderBy(p => p.EndDate)
                .Take(count)
                .ToListAsync(cancelltionToken);
        }





        public async Task Test()
        {
            var query = from project in _bugContext.Set<Project>()
                        join workflow in _bugContext.Set<Workflow>()
                        on project.WorkflowId equals workflow.Id
                        select new { project };
            var s = await query.ToListAsync();
        }


    }
}
