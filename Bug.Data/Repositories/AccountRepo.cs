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
    public class AccountRepo : EntityRepoBase<Account>, IAccountRepo
    {
        public AccountRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

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

        public override IQueryable<Account> SortOrder
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
