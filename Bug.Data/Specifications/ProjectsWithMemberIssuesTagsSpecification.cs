using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class ProjectsWithMemberIssuesTagsSpecification : BaseSpecification<Project>
    {
        public ProjectsWithMemberIssuesTagsSpecification
            (string memberId,
            int categoryId,
            string tagName)
            : base(p => p.Id != null)
        {
            AddInclude(p => p.Accounts.Where(
                a => a.Id == memberId));
            AddInclude(p => p.Tags.Where(
                t => t.Name == tagName &&
                t.CategoryId == categoryId));
            AddInclude(p => p.Issues);
            AddInclude("Issues.Tags");
        }
    }
    
}
