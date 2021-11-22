using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class AccountByIdCheckRoleSpecification : BaseSpecification<Account>
    {
        public AccountByIdCheckRoleSpecification(string id, int permission, string projectId)
            : base(a => a.Id == id &&
            a.Roles.AsQueryable().Any(r => r.Permissions.AsQueryable().Any(p => p.Id == permission)) &&
            a.Roles.AsQueryable().Any(r => r.Projects.AsQueryable().Any(p => p.Id == projectId)))
        {
            AddInclude(a => a.Timezone);
            AddInclude(a => a.Roles);
            AddInclude(a => a.Projects);
            AddInclude("Roles.Permissions");
            AddInclude("Roles.Projects");
        }
    }
}
