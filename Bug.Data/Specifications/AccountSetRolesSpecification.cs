using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class AccountSetRolesSpecification : BaseSpecification<Account>
    {
        public AccountSetRolesSpecification(string accountId, string projectId)
            : base(a=>a.Id == accountId &&
            a.AccountProjectRoles.AsQueryable().Any(apr=>apr.ProjectId == projectId))
        {
            AddInclude(a => a.AccountProjectRoles);
        }
    }
}
