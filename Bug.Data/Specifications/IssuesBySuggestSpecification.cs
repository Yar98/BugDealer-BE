using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class IssuesBySuggestSpecification : BaseSpecification<Issue>
    {
        public IssuesBySuggestSpecification(string search, string projectId)
            : base(i=>(i.Project.Code + "-" + i.Code).Contains(search) &&
            i.Project.Id == projectId)
        {
            AddInclude(i => i.Severity);
            AddInclude(i => i.Status);
            AddInclude(i => i.Priority);
            AddInclude(i => i.Project);
            AddInclude(i => i.Reporter);
            AddInclude(i => i.Assignee);

        }
    }
}
