using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class RoleSpecification : BaseSpecification<Role>
    {
        public RoleSpecification(int roleId)
            : base(r=>r.Id == roleId)
        {
            AddInclude(r => r.Permissions);
            AddInclude(r => r.Projects);
        }

    }
}
