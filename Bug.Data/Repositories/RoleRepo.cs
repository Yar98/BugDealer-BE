using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Core.Utility;
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

        public async Task<Role> GetRoleAsync
            (ISpecification<Role> specificationResult,
            CancellationToken cancellationToken = default)
        {
            return await _bugContext
                .Roles
                .Specify(specificationResult)
                .FirstOrDefaultAsync(cancellationToken);
        }
        
        public async Task<PaginatedList<Role>> GetPaginatedListAsync
            (int pageIndex,
            int pageSize,
            string sortOrder,
            ISpecification<Role> specificationResult,
            CancellationToken cancellationToken = default)
        {
            var result = _bugContext
                .Roles
                .Specify(specificationResult);
            SortOrder(result, sortOrder);
            return await PaginatedList<Role>
                .CreateListAsync(result, pageIndex, pageSize, cancellationToken);
        }
        
        public async Task<IReadOnlyList<Role>> GetNextByOffsetAsync
            (int offset,
            int next,
            string sortOrder,
            ISpecification<Role> specificationResult,
            CancellationToken cancellationToken = default)
        {
            var result = _bugContext
                .Roles
                .Specify(specificationResult);
            SortOrder(result, sortOrder);
            return await result
                .Skip(offset)
                .Take(next)
                .ToListAsync(cancellationToken);
        }

        private IQueryable<Role> SortOrder
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
}
