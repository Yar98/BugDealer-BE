using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class IssuelogSpecification : BaseSpecification<Issuelog>
    {
        public IssuelogSpecification(int id)
            :base(i=> i.Id == id)
        {
            AddInclude(i => i.Issue);
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
            AddInclude(i => i.OldToIssue);
            AddInclude(i => i.NewWorklog);
            AddInclude(i => i.OldWorklog);
            AddInclude(i => i.Tag);
        }
    }
}
