using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Core.Utils;
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
    }
}
