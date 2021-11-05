using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class IssueDetailLv1ByProjectSpecification : BaseSpecification<Issue>
    {
        public IssueDetailLv1ByProjectSpecification(string projectId)
            : base(i => i.ProjectId == projectId)
        {
            AddInclude(i => i.Status);
            AddInclude(i => i.Priority);
            AddInclude(i => i.Project);
            AddInclude(i => i.Reporter);
            AddInclude(i => i.Assignee);
            AddInclude(i => i.Tags);
        }
    }
}
