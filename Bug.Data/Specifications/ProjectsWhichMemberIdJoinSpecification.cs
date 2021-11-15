using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class ProjectsWhichMemberIdJoinSpecification : BaseSpecification<Project>
    {
        public ProjectsWhichMemberIdJoinSpecification
            (string memberId,
            int tagId)
            : base(p => p.Id != null && 
            p.Accounts.AsQueryable().Any(a=>a.Id == memberId) &&
            p.Tags.AsQueryable().Any(t=>t.Id == tagId))
        {
            AddInclude(p => p.Creator);
            AddInclude(p => p.DefaultAssignee);
            AddInclude(p => p.Accounts);
            AddInclude(p => p.Roles);
            AddInclude(p => p.Issues);
            AddInclude(p => p.Tags);
            AddInclude("Issues.Tags");
        }
    }
    
}
