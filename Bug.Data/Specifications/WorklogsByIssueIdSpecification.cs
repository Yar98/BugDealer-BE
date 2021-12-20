using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class WorklogsByIssueIdSpecification : BaseSpecification<Worklog>
    {
        public WorklogsByIssueIdSpecification(string issueId)
            : base(w=>w.IssueId == issueId)
        {
            
        }
    }
}
