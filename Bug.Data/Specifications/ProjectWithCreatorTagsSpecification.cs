using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class ProjectWithCreatorTagsSpecification : BaseSpecification<Project>
    {
        public ProjectWithCreatorTagsSpecification(string creatorId)
            : base(p => p.CreatorId == creatorId)
        {
            AddInclude(p => p.Tags);
        }
        public ProjectWithCreatorTagsSpecification(string accountId, 
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
