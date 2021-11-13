using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class RolesWhichMemberOnSpecification : BaseSpecification<Role>
    {
        public RolesWhichMemberOnSpecification(string projectId, string memberId)
            : base(r=>r.Accounts.AsQueryable().Where(a=>a.Id==memberId).Any() &&
            r.Projects.AsQueryable().Where(p=>p.Id==projectId).Any())
        {
            AddInclude(r => r.Accounts);
            AddInclude(r => r.CreatorId);
            AddInclude(r => r.Permissions);
            AddInclude(r => r.Projects);
        }
    }
}
