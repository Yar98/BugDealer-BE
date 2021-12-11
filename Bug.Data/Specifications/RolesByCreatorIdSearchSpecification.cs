using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class RolesByCreatorIdSearchSpecification : BaseSpecification<Role>
    {
        public RolesByCreatorIdSearchSpecification(string creatorId, string search)
            :base(r=>r.CreatorId == creatorId &&
            r.Name.Contains(search))
        {
            AddInclude(r => r.Permissions);
            AddInclude(r => r.Projects);
            AddInclude(r => r.AccountProjectRoles);
        }
    }
}
