using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class RoleDetailLv1ByProjectSpecification : BaseSpecification<Role>
    {
        public RoleDetailLv1ByProjectSpecification(string projectId)
            :base(r=>r.Id != null &&
            r.Projects.AsQueryable().Any(p=>p.Id==projectId))
        {
            AddInclude(r => r.Projects);
            AddInclude(r => r.Accounts);
            AddInclude(r => r.Permissions);
        }
    }
}
