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
    public interface IRoleRepo : IEntityRepoBase<Role>
    {
        Task<Role> GetRoleAsync
            (ISpecification<Role> specificationResult,
            CancellationToken cancellationToken = default);
        Task<PaginatedList<Role>> GetPaginatedListAsync
            (int pageIndex,
            int pageSize,
            string sortOrder,
            ISpecification<Role> specificationResult,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Role>> GetNextByOffsetAsync
            (int offset,
            int next,
            string sortOrder,
            ISpecification<Role> specificationResult,
            CancellationToken cancellationToken = default);
    }
}
