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
    public interface IAccountRepo : IEntityRepoBase<Account>
    {
        Task<Account> GetAccountByEmail(string email);
        Task<Account> GetAccountByUserName(string userName, string password);
        Task<Account> GetAccountAsync
            (ISpecification<Account> specificationResult,
            CancellationToken cancellationToken = default);
        Task<PaginatedList<Account>> GetPaginatedListAsync
            (int pageIndex,
            int pageSize,
            string sortOrder,
            ISpecification<Account> specificationResult,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Account>> GetNextByOffsetAsync
            (int offset,
            int next,
            string sortOrder,
            ISpecification<Account> specificationResult,
            CancellationToken cancellationToken = default);
    }
}
