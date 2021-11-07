using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class ProjectsWhichMemberJoinSpecification : BaseSpecification<Project>
    {
        public ProjectsWhichMemberJoinSpecification
            (string memberId,
            int categoryId,
            string tagName)
            : base(p => p.Id != null && 
            p.Accounts.AsQueryable().Any(a=>a.Id == memberId) &&
            p.Tags.AsQueryable().Any(t=>t.Name==tagName && t.CategoryId==categoryId))
        {
            AddInclude(p => p.Accounts);
            AddInclude(p => p.Tags);
            AddInclude(p => p.Issues);
            AddInclude("Issues.Tags");
        }
    }
    
}
