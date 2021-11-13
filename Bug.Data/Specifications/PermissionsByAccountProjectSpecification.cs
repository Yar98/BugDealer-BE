using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class PermissionsByAccountProjectSpecification : BaseSpecification<Permission>
    {
        public PermissionsByAccountProjectSpecification(string accountId, string projectId)
            : base(p=>p.Roles.AsQueryable().Where(
                r => r.Accounts.AsQueryable().Where(a => a.Id == accountId).Any() &&
                    r.Projects.AsQueryable().Where(p => p.Id == projectId).Any())
            .Any())
        {
            AddInclude(p => p.Roles);
            AddInclude("Roles.Accounts");
            AddInclude("Roles.Projects");
        }
    }
}
