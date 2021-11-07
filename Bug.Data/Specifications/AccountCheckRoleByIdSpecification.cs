using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class AccountCheckRoleByIdSpecification : BaseSpecification<Account>
    {
        public AccountCheckRoleByIdSpecification(string id)
            : base(a => a.Id == id)
        {
            AddInclude(a => a.Roles);
            AddInclude(a => a.Projects);
            AddInclude("Roles.Permissions");
        }
    }
}
