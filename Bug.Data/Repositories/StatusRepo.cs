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
using Bug.Core.Utility;

namespace Bug.Data.Repositories
{
    public class StatusRepo : EntityRepoBase<Status>, IStatusRepo
    {
        public StatusRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<Status> GetStatusAsync
            (ISpecification<Status> specificationResult,
            CancellationToken cancellationToken = default)
        {
            return await _bugContext
                .Statuses
                .Specify(specificationResult)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<PaginatedList<Status>> GetPaginatedIssuesAsync
            (int pageIndex,
            int pageSize,
            string sortOrder,
            ISpecification<Status> speicificationResult,
            CancellationToken cancellationToken = default)
        {
            var result = _bugContext
                .Statuses
                .Specify(speicificationResult);
            SortOrder(result, sortOrder);
            return await PaginatedList<Status>
                .CreateListAsync(result, pageIndex, pageSize, cancellationToken);
        }
        public async Task<IReadOnlyList<Status>> GetNextIssuesByOffsetAsync
            (int offset,
            int next,
            string sortOrder,
            ISpecification<Status> specificationResult,
            CancellationToken cancellationToken = default)
        {
            var result = _bugContext
                .Statuses
                .Specify(specificationResult);
            SortOrder(result, sortOrder);
            return await result
                .Skip(offset)
                .Take(next)
                .ToListAsync(cancellationToken);
        }


        private IQueryable<Status> SortOrder
            (IQueryable<Status> result,
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "name":
                    result = result.OrderBy(p => p.Name);
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
