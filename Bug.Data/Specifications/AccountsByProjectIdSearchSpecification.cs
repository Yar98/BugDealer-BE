using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class AccountsByProjectIdSearchSpecification : BaseSpecification<Account>
    {
        public AccountsByProjectIdSearchSpecification(string projectId, string search)
            : base(a=>a.AccountProjectRoles.AsQueryable().Any(apr=>apr.ProjectId==projectId) &&
            a.UserName.Contains(search))
        {
            AddInclude(a => a.Timezone);
            AddInclude(a => a.VoteIssues);
            AddInclude(a => a.WatchIssues);
        }
    }
}
