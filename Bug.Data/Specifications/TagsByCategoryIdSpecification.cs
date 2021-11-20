using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class TagsByCategoryIdSpecification : BaseSpecification<Tag>
    {
        public TagsByCategoryIdSpecification(int id)
            : base(t => t.CategoryId == id)
        {
            AddInclude(t => t.Issues);
            AddInclude(t => t.Category);
            AddInclude(t => t.Statuses);
        }
    }
}
