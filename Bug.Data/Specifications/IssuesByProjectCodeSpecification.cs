using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class IssuesByProjectCodeSpecification : BaseSpecification<Issue>
    {
        public IssuesByProjectCodeSpecification(int code, string projectCode, string accountId)
            : base(i=>i.NumberCode == code &&
            i.Project.Code.Contains(projectCode) && 
            i.Project.AccountProjectRoles.AsQueryable().Any(apr=>apr.AccountId==accountId))
        {
            AddInclude(i => i.Status);
            AddInclude(i => i.Priority);
            AddInclude(i => i.Project);
            AddInclude(i => i.Reporter);
            AddInclude(i => i.Assignee);
        }
    }
}
