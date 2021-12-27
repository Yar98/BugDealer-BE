using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class IssuelogsByIssueIdSpecification : BaseSpecification<Issuelog>
    {
        public IssuelogsByIssueIdSpecification(string issueId)
            : base(i=>i.IssueId == issueId)
        {
            AddInclude(i => i.Issue);
            AddInclude("Issue.Project");
            AddInclude(i => i.Modifier);
            AddInclude(i => i.NewPriority);
            AddInclude(i => i.OldPriority);
            AddInclude(i => i.NewStatusTag);
            AddInclude(i => i.OldStatusTag);
            AddInclude(i => i.NewSeverity);
            AddInclude(i => i.OldSeverity);
            AddInclude(i => i.OldAssignee);
            AddInclude(i => i.NewAssignee);
            AddInclude(i => i.NewReporter);
            AddInclude(i => i.OldReporter);
            AddInclude(i => i.NewToIssue);
            AddInclude("NewToIssue.Project");
            AddInclude(i => i.OldToIssue);
            AddInclude("OldToIssue.Project");
            AddInclude(i => i.NewWorklog);
            AddInclude(i => i.OldWorklog);
            AddInclude(i => i.Tag);
        }
        public IssuelogsByIssueIdSpecification(string issueId, int tagId)
            : base(i => i.IssueId == issueId && i.TagId == tagId)
        {
            AddInclude(i => i.Issue);
            AddInclude("Issue.Project");
            AddInclude(i => i.Modifier);
            AddInclude(i => i.NewPriority);
            AddInclude(i => i.OldPriority);
            AddInclude(i => i.NewStatusTag);
            AddInclude(i => i.OldStatusTag);
            AddInclude(i => i.NewSeverity);
            AddInclude(i => i.OldSeverity);
            AddInclude(i => i.OldAssignee);
            AddInclude(i => i.NewAssignee);
            AddInclude(i => i.NewReporter);
            AddInclude(i => i.OldReporter);
            AddInclude(i => i.NewToIssue);
            AddInclude("NewToIssue.Project");
            AddInclude(i => i.OldToIssue);
            AddInclude("OldToIssue.Project");
            AddInclude(i => i.NewWorklog);
            AddInclude(i => i.OldWorklog);
            AddInclude(i => i.Tag);
        }
    }
}
