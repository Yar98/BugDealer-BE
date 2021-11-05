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
    public class ProjectRepo : EntityRepoBase<Project>, IProjectRepo, IProjectIntegrationRepo<Project>
    {
        public ProjectRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {
            
        }

        public async Task<Project> GetProjectAsync
            (ISpecification<Project> specificationResult,
            CancellationToken cancelltionToken = default)
        {
            return await _bugContext
                .Projects
                .Specify(specificationResult)
                .FirstOrDefaultAsync(cancelltionToken);
                
        }
        public async Task<PaginatedList<Project>> GetPaginatedProjectsAsync
            (int pageIndex, 
            int pageSize,
            string sortOrder,
            ISpecification<Project> specificationResult,
            CancellationToken cancelltionToken = default)
        {
            var result = _bugContext
                .Projects
                .Specify(specificationResult);
            result = SortOrder(result, sortOrder);
            return await PaginatedList<Project>
                .CreateListAsync(result.AsNoTracking(),pageIndex,pageSize,cancelltionToken);
        }
        public async Task<IReadOnlyList<Project>> GetNextProjectsByOffsetAsync
            (int offset, 
            int next,
            string sortOrder,
            ISpecification<Project> specificationResult,
            CancellationToken cancelltionToken = default)
        {
            var result = _bugContext
                .Projects
                .Specify(specificationResult);
            result = SortOrder(result, sortOrder);
            return await result
                .Skip(offset)
                .Take(next)
                .AsNoTracking()
                .ToListAsync(cancelltionToken);
        }
        /*
        public async Task<IReadOnlyList<Project>> GetRecentProjects(
            string accountId,
            int categoryId, 
            string tagName, 
            int count,
            ISpecification<Project> specificationResult,
            CancellationToken cancelltionToken = default)
        {
            return await _bugContext
                .Projects
                .Specify(specificationResult)
                .OrderBy(p => p.EndDate)
                .Take(count)
                .ToListAsync(cancelltionToken);
        }
        */

        private IQueryable<Project> SortOrder
            (IQueryable<Project> result, 
            string sortOrder)
        {
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
            return result;
        }




        public async Task Test()
        {
            var query = from project in _bugContext.Set<Project>()
                        join workflow in _bugContext.Set<Workflow>()
                        on project.CreatorId equals workflow.Id
                        select new { project };
            var s = await query.ToListAsync();
        }


    }
}
