using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class CommentsByIssueIdSpecification : BaseSpecification<Comment>
    {
        public CommentsByIssueIdSpecification(string issueId)
            : base(c=>c.IssueId == issueId)
        {
            AddInclude(c => c.Issue);
            AddInclude(c => c.Account);
        }
    }
}
