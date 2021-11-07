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
    public interface IStatusRepo : IEntityRepoBase<Status>
    {
        Task<Status> GetStatusAsync
            (ISpecification<Status> specificationResult,
            CancellationToken cancellationToken = default);
        Task<PaginatedList<Status>> GetPaginatedIssuesAsync
            (int pageIndex,
            int pageSize,
            string sortOrder,
            ISpecification<Status> speicificationResult,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Status>> GetNextIssuesByOffsetAsync
            (int offset,
            int next,
            string sortOrder,
            ISpecification<Status> specificationResult,
            CancellationToken cancellationToken = default);
    }
}
