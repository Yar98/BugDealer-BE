using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Core.Utility;
using Bug.Data.Specifications;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public interface IIssueRepo : IEntityRepoBase<Issue>
    {
        Task<Issue> GetIssuelAsync
            (ISpecification<Issue> specificationResult,
            CancellationToken cancelltionToken = default);
        Task<PaginatedList<Issue>> GetPaginatedIssuesAsync
            (int pageIndex,
            int pageSize,
            string sortOrder,
            ISpecification<Issue> specificationResult,
            CancellationToken cancelltionToken = default);
        Task<IReadOnlyList<Issue>> GetByOffsetIssuesAsync
            (int offset,
            int next,
            string sortOrder,
            ISpecification<Issue> specificationResult,
            CancellationToken cancellationToken = default);
    }
}
