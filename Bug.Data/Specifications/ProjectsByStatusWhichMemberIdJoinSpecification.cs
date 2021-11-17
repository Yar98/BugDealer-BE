using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class ProjectsByStatusWhichMemberIdJoinSpecification : BaseSpecification<Project>
    {
        public ProjectsByStatusWhichMemberIdJoinSpecification
            (string memberId,
            int tagId)
            : base(p => p.Id != null && 
            p.Accounts.AsQueryable().Any(a=>a.Id == memberId) &&
            p.Status == tagId)
        {
            AddInclude(p => p.Creator);
            AddInclude(p => p.DefaultAssignee);
            AddInclude(p => p.Template);
            AddInclude(p => p.Accounts);
            AddInclude(p => p.Roles);
            AddInclude(p => p.Issues);
            AddInclude("Issues.Tags");
        }
    }
    
}
