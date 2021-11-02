using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class ProjectByCreatorWithTagsIssuesSpecification : BaseSpecification<Project>
    {
        public ProjectByCreatorWithTagsIssuesSpecification(string accountId)
            : base(p=>p.CreatorId == accountId)
        {
            AddInclude(p => p.Issues.SelectMany(i => i.Tags));
        }
        public ProjectByCreatorWithTagsIssuesSpecification(string accountId,
            int categoryId,
            string tagName)
            : base(p => p.CreatorId == accountId)
        {
            AddInclude(p => p.Tags.Where(
                    t => t.Name == tagName &&
                    t.CategoryId == categoryId));
            AddInclude(p => p.Issues);
            AddInclude("Issues.Tags");
        }
    }
}
