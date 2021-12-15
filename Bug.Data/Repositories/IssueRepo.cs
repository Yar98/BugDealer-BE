using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Bug.Data.Specifications;
using System.Threading;
using Bug.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Bug.Core.Utils;
using Microsoft.Data.SqlClient;
using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Amazon;

namespace Bug.Data.Repositories
{
    public class IssueRepo : EntityRepoBase<Issue>, IIssueRepo
    {
        public IssueRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<List<Issue>> GetSuggestIssuesAsync
            (string projectId, 
            string search,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var s = new SqlParameter("search", "%" + search + "%");
            var project = new SqlParameter("project", projectId);
            var order = new SqlParameter("order", sortOrder);
            return await _bugContext
                .Issues
                .FromSqlRaw("EXECUTE dbo.GetSuggestIssues @project, @search, @order", project, s, order)
                .ToListAsync(cancellationToken);
        }

        public void UpdateIssuesHaveDumbStatus(List<Status> statuses)
        {
            if (statuses == null)
                return;
            var statusIds = statuses
                .Select(s => s.Id)
                .ToList()
                .Aggregate((x, y) => x + "," + y);
            var list = new SqlParameter("list", statusIds);
            _bugContext
                .Database
                .ExecuteSqlRaw("EXECUTE dbo.UpdateIssuesHaveDumbStatus @list", list);
        }

        public override IQueryable<Issue> SortOrder
            (IQueryable<Issue> result,
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
                    result = result.OrderBy(p => p.Id);
                    break;
            }
            return result;
        }

    }
}
