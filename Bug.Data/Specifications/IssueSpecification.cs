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
            AddInclude(i => i.Status);
            AddInclude(i => i.Priority);
            AddInclude(i => i.Project);
            AddInclude(i => i.Reporter);
            AddInclude(i => i.Assignee);
            AddInclude(i => i.Tags);
            AddInclude(i => i.FromRelations);
            AddInclude(i => i.ToRelations);
            AddInclude(i => i.Attachments);
            AddInclude("FromRelations.Tag");
            AddInclude("FromRelations.FromIssue");
            AddInclude("FromRelations.ToIssue");
            AddInclude("ToRelations.Tag");
            AddInclude("ToRelations.FromIssue");
            AddInclude("ToRelations.ToIssue");
        }
    }
}
