using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class IssuesByReporterIdProjectIdSpecification : BaseSpecification<Issue>
    {
        public IssuesByReporterIdProjectIdSpecification(string projectId, string reporterId)
            : base(i => i.ProjectId == projectId && i.ReporterId == reporterId)
        {
            AddInclude(i => i.Severity);
            AddInclude(i => i.Status);
            AddInclude(i => i.Priority);
            AddInclude(i => i.Project);
            AddInclude(i => i.Reporter);
            AddInclude(i => i.Assignee);
            AddInclude(i => i.Tags);
            AddInclude("Status.Tag");
        }
    }
}
