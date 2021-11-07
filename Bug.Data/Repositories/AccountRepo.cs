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
    public class AccountRepo : EntityRepoBase<Account>, IAccountRepo
    {
        public AccountRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }
        public async Task<Account> GetAccountAsync
            (ISpecification<Account> specificationResult,
            CancellationToken cancellationToken = default)
        {
            return await _bugContext
                .Accounts
                .Specify(specificationResult)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            return await _bugContext
                .Accounts
                .FirstOrDefaultAsync(a=>a.Email.Equals(email));
        }
        public async Task<Account> GetAccountByUserName
            (string userName, 
            string password)
        {
            return await _bugContext
                .Accounts
                .FirstOrDefaultAsync(
                a => a.UserName == userName && 
                a.Password == password);
        }

        public async Task<PaginatedList<Account>> GetPaginatedListAsync
            (int pageIndex,
            int pageSize,
            string sortOrder,
            ISpecification<Account> specificationResult,
            CancellationToken cancellationToken = default)
        {
            var result = _bugContext.Accounts.Specify(specificationResult);
            SortOrder(result,sortOrder);
            return await PaginatedList<Account>
                .CreateListAsync(result, pageIndex, pageSize, cancellationToken);
        }

        public async Task<IReadOnlyList<Account>> GetNextByOffsetAsync
            (int offset,
            int next, 
            string sortOrder,
            ISpecification<Account> specificationResult,
            CancellationToken cancellationToken = default)
        {
            var result = _bugContext.Accounts.Specify(specificationResult);
            SortOrder(result, sortOrder);
            return await result
                .Skip(offset)
                .Take(next)
                .ToListAsync(cancellationToken);
        }

        private IQueryable<Account> SortOrder
            (IQueryable<Account> result,
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
