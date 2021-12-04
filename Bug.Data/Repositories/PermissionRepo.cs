using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace Bug.Data.Repositories
{
    public class PermissionRepo : EntityRepoBase<Permission>, IPermissionRepo
    {
        public PermissionRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<List<Permission>> GetPermissionsFromMutiIdsAsync
            (List<int> list,
            CancellationToken cancellationToken = default)
        {
            var result = await _bugContext
                .Permissions
                .AsQueryable()
                .Where(p=>list.Contains(p.Id))
                .ToListAsync(cancellationToken);
            return result;
        }

        public override IQueryable<Permission> SortOrder
            (IQueryable<Permission> result,
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
