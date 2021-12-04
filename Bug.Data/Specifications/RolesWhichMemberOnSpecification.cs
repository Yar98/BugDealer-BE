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
            : base(r => r.AccountProjectRoles.AsQueryable().Where(
                p => p.ProjectId == projectId).Any() &&
            r.AccountProjectRoles.AsQueryable().Where(
                apr => apr.AccountId == memberId).Any())
        {
            AddInclude(r => r.CreatorId);
            AddInclude(r => r.Permissions);
            AddInclude(r => r.Projects);
            AddInclude(r => r.AccountProjectRoles);
        }
    }
}
