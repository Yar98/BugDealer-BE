using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class RoleByCreatorSpecification : BaseSpecification<Role>
    {
        public RoleByCreatorSpecification(string creatorId)
            : base(r => r.Creator.Id==creatorId)
        {
            AddInclude(r => r.Permissions);
            AddInclude(r => r.Creator);
            AddInclude(r => r.Accounts);
        }
    }
}
