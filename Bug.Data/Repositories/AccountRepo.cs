using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
