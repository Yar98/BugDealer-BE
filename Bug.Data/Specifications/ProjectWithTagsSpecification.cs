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
            : base(b => b.CreatorId == accountId)
        {
            AddInclude(b => b.Tags);
        }
        public ProjectWithTagsSpecification(string accountId, 
            int categoryId, 
            string tagName)
            : base(b => b.CreatorId == accountId)
        {            
            AddInclude(b => b.Tags.Where(
                t=>t.Name==tagName && 
                t.CategoryId==categoryId));
        }
    }
}
