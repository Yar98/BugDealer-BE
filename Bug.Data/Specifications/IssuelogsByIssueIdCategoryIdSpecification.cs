using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class IssuelogsByIssueIdCategoryIdSpecification : BaseSpecification<Issuelog>
    {
        public IssuelogsByIssueIdCategoryIdSpecification(string issueId, int categoryId)
            : base(i => i.IssueId == issueId && i.Tag.CategoryId == categoryId)
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
