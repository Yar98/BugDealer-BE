using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class PermissionsDetailLv1ByRoleProjectSpecification : BaseSpecification<Permission>
    {
        public PermissionsDetailLv1ByRoleProjectSpecification(string roleId, string projectId)
            : base(p=>p.Roles.AsQueryable().Where(
                r=>r.Id==roleId &&
                r.Projects.AsQueryable().Where(p=>p.Id==projectId).Any())
            .Any())
        {
            AddInclude(p => p.Roles);
            AddInclude("Roles.Projects");
        }
    }
}
