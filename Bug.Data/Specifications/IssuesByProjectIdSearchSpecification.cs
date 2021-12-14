using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class IssuesByProjectIdSearchSpecification : BaseSpecification<Issue>
    {
        public IssuesByProjectIdSearchSpecification(string projectId, string search)
            : base(i=>i.ProjectId == projectId &&
            ((i.Project.Code+"-"+i.Code).Contains(search) || i.Title.Contains(search) || i.Status.Name.Contains(search) || i.Assignee.FirstName.Contains(search) || i.Reporter.FirstName.Contains(search) || i.Reporter.LastName.Contains(search) || i.Assignee.FirstName.Contains(search)))
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
