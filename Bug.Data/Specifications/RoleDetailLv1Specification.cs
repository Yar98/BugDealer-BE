using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class RoleDetailLv1Specification : BaseSpecification<Role>
    {
        public RoleDetailLv1Specification(string roleId)
            : base(r=>r.Id == roleId)
        {
            AddInclude(r => r.Permissions);
            AddInclude(r => r.Accounts);
            AddInclude(r => r.Projects);
        }
    }
}
