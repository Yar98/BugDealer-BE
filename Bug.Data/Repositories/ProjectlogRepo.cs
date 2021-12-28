using Bug.Entities.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.Data.Repositories
{
    public class ProjectlogRepo : EntityRepoBase<Projectlog>, IProjectlogRepo
    {
        public ProjectlogRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<IReadOnlyList<Projectlog>> GetRecentAsync
            (string accountId,
            int offset,
            int next,
            CancellationToken cancellationToken = default)
        {
            var sql = "SELECT i.* FROM [Projectlog] AS i " +
                "WHERE i.ModifierId = @accountId AND i.Id IN " +
                "(SELECT tem.id FROM " +
                "(SELECT Id, ROW_NUMBER() OVER (PARTITION BY ProjectId, ModifierId ORDER BY LogDate DESC) As d FROM [Projectlog]) AS tem " +
                "WHERE tem.d = 1) " +
                "ORDER BY i.LogDate DESC " +
                "OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY";
            var accountSql = new SqlParameter("accountId", accountId);
            var offsetSql = new SqlParameter("offset", offset);
            var nextSql = new SqlParameter("next", next);
            return await _bugContext
                .Projectlogs
                .FromSqlRaw(sql,accountSql,offsetSql,nextSql)
                .Include(l=>l.Modifier)                
                .Include(l => l.Project)
                .ThenInclude(p => p.Issues)
                .ThenInclude(i=>i.Status)
                .Include(l => l.Project)
                .ThenInclude(p => p.Template)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public override IQueryable<Projectlog> SortOrder(IQueryable<Projectlog> result, string sortOrder)
        {
            switch (sortOrder)
            {
                case "name":
                    result = result.OrderBy(p => p.ModifierId);
                    break;
                case "logdate_desc":
                    result = result.OrderByDescending(p => p.LogDate);
                    break;
                default:
                    result = result.OrderBy(p => p.LogDate);
                    break;
            }
            return result;
        }
    }
}
