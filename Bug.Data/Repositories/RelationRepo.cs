using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Bug.Data.Repositories
{
    public class RelationRepo : EntityRepoBase<Relation>, IRelationRepo
    {
        public RelationRepo(BugContext repositoryContext)
             : base(repositoryContext)
        {

        }

        public async Task DeleteRelationByIssueAsync
            (string issueId,
            CancellationToken cancellationToken = default)
        {
            await _bugContext
                .Database
                .ExecuteSqlRawAsync("EXECUTE dbo.DeleteRelationByIssue @issue = '"+ issueId + "'", cancellationToken);
        }

        public override IQueryable<Relation> SortOrder
            (IQueryable<Relation> result,
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "name":
                    //result = result.OrderBy(p => p.Name);
                    break;
                case "startdate":
                    //result = result.OrderBy(p => p.StartDate);
                    break;
                case "startdate_desc":
                    //result = result.OrderByDescending(p => p.StartDate);
                    break;
                case "enddate":
                    //result = result.OrderBy(p => p.EndDate);
                    break;
                case "enddate_desc":
                    //result = result.OrderByDescending(p => p.EndDate);
                    break;
                case "recentdate":
                    //result = result.OrderBy(p => p.RecentDate);
                    break;
                case "recentdate_desc":
                    //result = result.OrderByDescending(p => p.RecentDate);
                    break;
                default:
                    result = result.OrderBy(p => p.FromIssueId);
                    break;
            }
            return result;
        }
    }
}
