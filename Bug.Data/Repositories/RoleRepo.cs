using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Core.Utils;
using Bug.Data.Extensions;
using Bug.Data.Specifications;
using Bug.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace Bug.Data.Repositories
{
    public class RoleRepo : EntityRepoBase<Role>, IRoleRepo
    {
        public RoleRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<List<Role>> GetRolesFromMutiIdsAsync
            (List<int> list,
            CancellationToken cancellationToken = default)
        {
            var result = await _bugContext
                .Roles
                .AsQueryable()
                .Where(p => list.Contains(p.Id))
                .ToListAsync(cancellationToken);
            return result;
        }

        public async Task<IReadOnlyList<Role>> GetDefaultRolesNoTrackAsync
            (string creatorId = "bts",
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RoleByCreatorIdSpecification(creatorId);
            return await GetNextByOffsetNoTrackBySpecAsync
                (0, 10, null, specificationResult, cancellationToken);

        }

        public async Task<IReadOnlyList<Role>> GetDefaultRolesAsync
            (string creatorId = "bts",
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RoleByCreatorIdSpecification(creatorId);
            return await GetNextByOffsetBySpecAsync
                (0, 10, null, specificationResult, cancellationToken);

        }

        public override IQueryable<Role> SortOrder
            (IQueryable<Role> result,
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
