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

namespace Bug.Data.Repositories
{
    public class ProjectRepo : EntityRepoBase<Project>, IProjectRepo
    {
        public ProjectRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {
            
        }

        public async Task<Project> GetDetailProject(string projectId)
        {
            return await _bugContext
                .Projects
                .SingleOrDefaultAsync(p => p.Id == projectId);
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
