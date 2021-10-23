using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class ProjectRecentSpecification : BaseSpecification<Project>
    {
        public ProjectRecentSpecification(string accountId)
            : base(b => b.CreatorId == accountId)
        {
            AddInclude(b => b.Tags);
        }
        public ProjectRecentSpecification(string accountId, 
            int categoryId, 
            string tagName)
            : base(b => b.CreatorId == accountId)
        {            
            AddInclude(b => b.Tags.Where(
                t=>t.Name == tagName && 
                t.CategoryId == categoryId));
        }
    }
}
