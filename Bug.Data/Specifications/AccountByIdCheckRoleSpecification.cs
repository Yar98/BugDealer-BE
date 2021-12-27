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
            a.AccountProjectRoles.AsQueryable().Any(
                apr=>apr.ProjectId == projectId) &&
            a.AccountProjectRoles.AsQueryable().Any(
                apr=>apr.Role.Permissions.AsQueryable().Any(p=>p.Id==permission)))
        {
            AddInclude(a => a.AccountProjectRoles);
            AddInclude("AccountProjectRoles.Role.Permissions");
        }
    }
}
