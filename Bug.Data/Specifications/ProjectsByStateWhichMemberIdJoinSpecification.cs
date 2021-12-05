using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class ProjectsByStateWhichMemberIdJoinSpecification : BaseSpecification<Project>
    {
        public ProjectsByStateWhichMemberIdJoinSpecification
            (string memberId,
            int state)
            : base(p => p.Id != null && 
            p.AccountProjectRoles.AsQueryable().Any(apr=>apr.AccountId == memberId) &&
            p.State == state)
        {
            AddInclude(p => p.Creator);
            AddInclude(p => p.DefaultAssignee);
            AddInclude(p => p.Template);
            AddInclude(p => p.Roles);
            AddInclude(p => p.Statuses);
            AddInclude(p => p.Issues);
            AddInclude(p => p.AccountProjectRoles);
            AddInclude("Issues.Tags");
            AddInclude("Issues.Status");            
        }
    }
    
}
