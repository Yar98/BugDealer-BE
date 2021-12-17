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
            AddInclude(i => i.Modifier);
            AddInclude(i => i.NewPriority);
            AddInclude(i => i.OldPriority);
            AddInclude(i => i.Tag);
        }
        public IssuelogsByIssueIdSpecification(string issueId, int tagId)
            : base(i => i.IssueId == issueId && i.TagId == tagId)
        {
            AddInclude(i => i.Issue);
            AddInclude(i => i.Modifier);
            AddInclude(i => i.NewPriority);
            AddInclude(i => i.OldPriority);
            AddInclude(i => i.Tag);
        }
    }
}
