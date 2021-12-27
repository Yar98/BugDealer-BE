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
using Bug.Core.Utils;

namespace Bug.Data.Repositories
{
    public class ProjectRepo : EntityRepoBase<Project>, IProjectRepo
    {
        public ProjectRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {
            
        }

        public async Task DeleteProject
            (string issueId,
            string projectId,
            CancellationToken cancellationToken = default)
        {
            var sql = "EXECUTE dbo.DeleteProject @issue = '" + issueId + "'";
            await _bugContext
                .Database
                .ExecuteSqlRawAsync(sql, cancellationToken);
        }

        public override IQueryable<Project> SortOrder
            (IQueryable<Project> result, 
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "name":
                    result = result.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    result = result.OrderByDescending(p => p.Name);
                    break;
                case "code":
                    result = result.OrderBy(p => p.Code);
                    break;
                case "code_desc":
                    result = result.OrderByDescending(p => p.Code);
                    break;
                case "type":
                    result = result.OrderBy(p => p.Template.Name);
                    break;
                case "type_desc":
                    result = result.OrderByDescending(p => p.Template.Name);
                    break;
                case "description":
                    result = result.OrderBy(p => p.Description);
                    break;
                case "description_desc":
                    result = result.OrderByDescending(p => p.Description);
                    break;
                default:
                    result = result.OrderBy(p => p.Id);
                    break;
            }
            return result;
        }



    }
}
