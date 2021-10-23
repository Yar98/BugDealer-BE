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
        public IQueryable<Project> GetRecentProject(string accountId,
            int categoryId,
            string tagName,
            int count)
        {
            var specificationResult = 
                new ProjectWithTagsSpecification(accountId, categoryId, tagName);
            return _bugContext
                .Specify(specificationResult)              
                .OrderBy(p => p.EndDate)
                .Take(count);
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
