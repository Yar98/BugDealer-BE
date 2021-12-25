using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class RolesByProjectIdSearchSpecification : BaseSpecification<Role>
    {
        public RolesByProjectIdSearchSpecification(string projectId, string search)
            : base(r=>r.Projects.AsQueryable().Any(apr=>apr.Id==projectId) &&
            r.Name.Contains(search))
        {
            AddInclude(r => r.Permissions);
            AddInclude(r => r.Projects);
            AddInclude(r => r.AccountProjectRoles);
            AddInclude("AccountProjectRoles.Project");
            AddInclude("AccountProjectRoles.Account");
        }
    }
}
