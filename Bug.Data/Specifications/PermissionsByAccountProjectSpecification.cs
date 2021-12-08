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
                r => r.AccountProjectRoles.AsQueryable().Where(
                    p => p.ProjectId == projectId).Any() &&
                r.AccountProjectRoles.AsQueryable().Where(
                    apr=>apr.AccountId == accountId).Any())
            .Any())
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.Roles);
            AddInclude("Roles.AccountProjectRoles");
        }
    }
}
