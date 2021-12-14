using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class IssuesByRelateUserSpecification : BaseSpecification<Issue>
    {
        public IssuesByRelateUserSpecification(string accountId)
            :base(i=>i.ReporterId == accountId && 
            i.AssigneeId == accountId &&
            i.Watchers.AsQueryable().Any(a => a.Id == accountId))
        {
            AddInclude(i => i.Severity);
            AddInclude(i => i.Status);
            AddInclude(i => i.Priority);
            AddInclude(i => i.Project);
            AddInclude(i => i.Reporter);
            AddInclude(i => i.Assignee);
            AddInclude(i => i.Tags);
        }
    }
}
