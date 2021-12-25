using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Bug.Data.Repositories
{
    public class IssuelogRepo : EntityRepoBase<Issuelog>, IIssuelogRepo
    {
        public IssuelogRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<IReadOnlyList<Issuelog>> GetRecentAsync
            (string accountId,
            int offset,
            int next,
            CancellationToken cancellationToken = default)
        {
            var sql = "SELECT i.* FROM [Issuelog] AS i " +
                "WHERE i.ModifierId = @accountId AND i.Id IN " +
                "(SELECT tem.id FROM " +
                "(SELECT Id, ROW_NUMBER() OVER (PARTITION BY IssueId ORDER BY LogDate) As d FROM [Issuelog]) AS tem " +
                "WHERE tem.d = 1) " +
                "ORDER BY i.LogDate DESC " +
                "OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY";
            var accountSql = new SqlParameter("accountId", accountId);
            var offsetSql = new SqlParameter("offset", offset);
            var nextSql = new SqlParameter("next", next);
            return await _bugContext
                .Issuelogs
                .FromSqlRaw(sql, accountSql, offsetSql, nextSql)
                .Include(l=>l.Issue)
                .Include(l=>l.Tag)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteLogBeforeDelIssue
            (string issueId,
            CancellationToken cancellationToken = default)
        {
            var sql = "EXECUTE dbo.DeleteLogBeforeDelIssue @issue = '" + issueId + "'";
            await _bugContext
                .Database
                .ExecuteSqlRawAsync(sql, cancellationToken);
        }

        public override IQueryable<Issuelog> SortOrder
            (IQueryable<Issuelog> result,
            string sortOrder)
        {
            var eql = new IssuelogEqualityComparer();
            switch (sortOrder)
            {
                case "logdate_desc":
                    result = result.OrderByDescending(p => p.LogDate);
                    break;
                case "startdate":
                    //result = result.OrderBy(p => p.StartDate);
                    break;
                case "startdate_desc":
                    //result = result.OrderByDescending(p => p.StartDate);
                    break;
                case "enddate":
                //result = result.OrderBy(p => p.EndDate);
                //break;
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

    public class IssuelogEqualityComparer : IEqualityComparer<Issuelog>
    {
        public bool Equals(Issuelog x, Issuelog y)
        {
            if (x.ModifierId == y.ModifierId)
                return true;
            return false;
        }

        public int GetHashCode([DisallowNull] Issuelog obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
