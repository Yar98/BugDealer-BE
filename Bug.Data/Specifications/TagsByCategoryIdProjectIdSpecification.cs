using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class TagsByCategoryIdProjectIdSpecification : BaseSpecification<Tag>
    {
        public TagsByCategoryIdProjectIdSpecification(string projectId, int id)
            : base(t => t.CategoryId == id && 
            t.Projects.AsQueryable().Where(p=>p.Id == projectId).Any())
        {
            AddInclude(t => t.Projects);
            AddInclude(t => t.Category);
            AddInclude(t => t.Statuses);
        }
    }
}
