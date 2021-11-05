using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class StatusDetailLv1Specification : BaseSpecification<Status>
    {
        public StatusDetailLv1Specification(string statusId)
            : base(s=>s.Id == statusId)
        {
            AddInclude(s => s.Tags);
            AddInclude(s => s.Accounts);
        }
    }
}
