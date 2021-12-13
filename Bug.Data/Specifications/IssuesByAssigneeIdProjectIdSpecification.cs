using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class IssuesByAssigneeIdProjectIdSpecification : BaseSpecification<Issue>
    {
        public IssuesByAssigneeIdProjectIdSpecification(string projectId,string assigneeId)
            :base(i=>i.ProjectId == projectId && i.AssigneeId == assigneeId)
        {
            AddInclude(i => i.Status);
            AddInclude(i => i.Priority);
            AddInclude(i => i.Project);
            AddInclude(i => i.Reporter);
            AddInclude(i => i.Assignee);
            AddInclude(i => i.Tags);
            //AddInclude(i => i.FromRelations);
            //AddInclude(i => i.ToRelations);
            //AddInclude("FromRelations.Tag");
            //AddInclude("FromRelations.FromIssue");
            //AddInclude("FromRelations.ToIssue");
            //AddInclude("ToRelations.Tag");
            //AddInclude("ToRelations.FromIssue");
            //AddInclude("ToRelations.ToIssue");
        }
    }
}
