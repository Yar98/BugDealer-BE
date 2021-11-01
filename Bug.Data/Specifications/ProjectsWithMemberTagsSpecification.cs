using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class ProjectsWithMemberTagsSpecification : BaseSpecification<Project>
    {
        public ProjectsWithMemberTagsSpecification(string memberId, int categoryId,
            string tagName)
            : base(p=>p.Id != null)
        {
            AddInclude(p => p.Accounts.Where(
                a => a.Id == memberId));
            AddInclude(p => p.Tags.Where(
                t => t.Name == tagName &&
                t.CategoryId == categoryId));
        }
    }
}
