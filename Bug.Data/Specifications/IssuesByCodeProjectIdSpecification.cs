using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class IssuesByCodeProjectIdSpecification : BaseSpecification<Issue>
    {
        public IssuesByCodeProjectIdSpecification(string code, string projectId)
            : base(i=>i.Code.Contains(code) &&
            i.ProjectId == projectId)
        {
            AddInclude(i => i.Status);
            AddInclude(i => i.Priority);
            AddInclude(i => i.Project);
            AddInclude(i => i.Reporter);
            AddInclude(i => i.Assignee);
        }
    }
}
