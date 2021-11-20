using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class TagSpecification : BaseSpecification<Tag>
    {
        public TagSpecification(int id)
            :base(t=>t.Id == id)
        {
            AddInclude(t => t.Issues);
            AddInclude(t => t.Category);
            AddInclude(t => t.Statuses);
        }
    }
}
