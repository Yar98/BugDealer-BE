using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class ProjectWithTagsSpecification : BaseSpecification<Project>
    {
        public ProjectWithTagsSpecification(string accountId)
            : base(p => p.CreatorId == accountId)
        {
            AddInclude(p => p.Tags);
        }
        public ProjectWithTagsSpecification(string accountId, 
            int categoryId,
            string tagName)
            : base(p => p.CreatorId == accountId)
        {
            AddInclude(p => p.Tags.Where(
                    t => t.Name == tagName &&
                    t.CategoryId == categoryId));
        }
    }
}
