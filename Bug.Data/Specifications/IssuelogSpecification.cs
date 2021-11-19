using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class IssuelogSpecification : BaseSpecification<Issuelog>
    {
        public IssuelogSpecification(int id)
            :base(i=> i.Id == id)
        {
            AddInclude(i => i.Issue);
            AddInclude(i => i.Modifier);
            AddInclude(i => i.ModPriority);
            AddInclude(i => i.PrePriority);
            AddInclude(i => i.PreStatus);
            AddInclude(i => i.ModStatus);
            AddInclude(i => i.Tag);
        }
    }
}
