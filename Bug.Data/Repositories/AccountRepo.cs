using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Repositories
{
    public class AccountRepo : EntityRepoBase<Class1>, IAccountRepo
    {
        public AccountRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
