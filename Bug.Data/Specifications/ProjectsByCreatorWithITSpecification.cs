using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class ProjectsByCreatorWithITSpecification : BaseSpecification<Project>
    {
        public ProjectsByCreatorWithITSpecification(string accountId)
            : base(p=>p.CreatorId == accountId)
        {
            AddInclude(p => p.Issues.SelectMany(i => i.Tags));
        }
        public ProjectsByCreatorWithITSpecification
            (string accountId,
            int categoryId,
            string tagName)
            : base(p => p.CreatorId == accountId && 
            p.Tags.AsQueryable().Any(t=>t.Name==tagName && t.CategoryId==categoryId))
        {
            AddInclude(p => p.Tags);
            AddInclude(p => p.Issues);
            AddInclude("Issues.Tags");
        }
    }
}
