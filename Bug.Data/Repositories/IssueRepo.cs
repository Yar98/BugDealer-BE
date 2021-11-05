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
    public class IssueRepo : EntityRepoBase<Issue>, IIssueRepo
    {
        public IssueRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<Issue> GetIssuelAsync
            (ISpecification<Issue> specificationResult,
            CancellationToken cancelltionToken = default)
        {
            return await _bugContext
                .Issues
                .Specify(specificationResult)
                .FirstOrDefaultAsync(cancelltionToken);
        }
        public async Task<PaginatedList<Issue>> GetPaginatedIssuesAsync
            (int pageIndex,
            int pageSize,
            string sortOrder,
            ISpecification<Issue> specificationResult,
            CancellationToken cancelltionToken = default)
        {
            var result = _bugContext
                .Issues
                .Specify(specificationResult);
            result = SortOrder(result, sortOrder);
            return await PaginatedList<Issue>
                .CreateListAsync(result.AsNoTracking(), pageIndex, pageSize, cancelltionToken);
        }
        public async Task<IReadOnlyList<Issue>> GetByOffsetIssuesAsync
            (int offset,
            int next,
            string sortOrder,
            ISpecification<Issue> specificationResult,
            CancellationToken cancellationToken = default)
        {
            var result = _bugContext
                .Issues
                .Specify(specificationResult);
            result = SortOrder(result, sortOrder);
            return await result
                .Skip(offset)
                .Take(next)
                .ToListAsync(cancellationToken);
        }

        private IQueryable<Issue> SortOrder
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
