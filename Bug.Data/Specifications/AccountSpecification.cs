using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class AccountSpecification : BaseSpecification<Account>
    {
        public AccountSpecification(string id)
            :base(a=>a.Id == id)
        {
            AddInclude(a => a.Timezone);
            AddInclude(a => a.VoteIssues);
            AddInclude(a => a.WatchIssues);
            AddInclude(a => a.Fields);
        }
    }
}
