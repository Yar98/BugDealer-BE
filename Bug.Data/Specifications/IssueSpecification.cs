using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class IssueSpecification : BaseSpecification<Issue>
    {
        public IssueSpecification(string issueId)
            : base(i=>i.Id == issueId)
        {
            AddInclude(i => i.Severity);
            AddInclude(i => i.Status);
            AddInclude(i => i.Priority);
            AddInclude(i => i.Project);
            AddInclude(i => i.Reporter);
            AddInclude(i => i.Assignee);
            AddInclude(i => i.Tags);
            AddInclude(i => i.FromRelations);
            //AddInclude(i => i.ToRelations);
            AddInclude(i => i.Attachments);
            AddInclude(i => i.Voters);
            AddInclude(i => i.Watchers);
            AddInclude("Status.Tag");
        }

        public IssueSpecification(string issueId, int count)
            : base(i => i.Id == issueId)
        {
            if(count == 1)
                AddInclude(i => i.Tags);
            if (count == 2)
                AddInclude(i => i.Watchers);
            if (count == 3)
                AddInclude(i => i.Voters);
            if (count == 4)
                AddInclude(i => i.Project);
        }
    }
}
