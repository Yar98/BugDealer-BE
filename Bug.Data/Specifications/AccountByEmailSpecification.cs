using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class AccountByEmailSpecification : BaseSpecification<Account>
    {
        public AccountByEmailSpecification(string email)
            : base(a => a.Email == email)
        {
            AddInclude(a => a.Timezone);
            AddInclude(a => a.AccountProjectRoles);
        }
    }
}
