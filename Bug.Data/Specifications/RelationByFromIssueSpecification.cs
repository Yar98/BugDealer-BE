using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class RelationByFromIssueSpecification : BaseSpecification<Relation>
    {
        public RelationByFromIssueSpecification(string issueId)
            : base(r=>r.FromIssueId == issueId)
        {
            AddInclude(r => r.ToIssue);
            AddInclude("ToIssue.Status");
            AddInclude("ToIssue.Project");
            AddInclude("ToIssue.Assignee");
            AddInclude(r => r.Tag);
        }
    }
}
