using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class RoleByProjectSpecification : BaseSpecification<Role>
    {
        public RoleByProjectSpecification(string projectId)
            :base(r=>r.Id != 0 &&
            r.Projects.AsQueryable().Any(p=>p.Id==projectId))
        {
            AddInclude(r => r.Projects);
            AddInclude(r => r.Permissions);
        }

    }
}
