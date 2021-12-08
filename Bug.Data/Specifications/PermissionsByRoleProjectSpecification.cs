using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class PermissionsByRoleProjectSpecification : BaseSpecification<Permission>
    {
        public PermissionsByRoleProjectSpecification(int roleId, string projectId)
            : base(p=>p.Roles.AsQueryable().Where(
                r=>r.Id==roleId &&
                r.Projects.AsQueryable().Where(p=>p.Id==projectId).Any())
            .Any())
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.Roles);
            AddInclude("Roles.Projects");
        }
    }
}
