using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class AccountsByProjectIdSpecification : BaseSpecification<Account>
    {
        public AccountsByProjectIdSpecification(string projectId)
            : base(m=>m.Id != null && 
            m.AccountProjectRoles.AsQueryable().Any(apr=>apr.ProjectId == projectId))
        {
            AddInclude(a => a.AccountProjectRoles);
            AddInclude("AccountProjectRoles.Role");
            AddInclude("AccountProjectRoles.Project");
        }
    }
}
