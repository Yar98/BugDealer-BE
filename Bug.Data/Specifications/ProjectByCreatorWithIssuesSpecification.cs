using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class ProjectByCreatorWithIssuesSpecification : BaseSpecification<Project>
    {
        public ProjectByCreatorWithIssuesSpecification(string accountId)
            : base(p=>p.CreatorId == accountId)
        {
            AddInclude(p => p.Issues.SelectMany(i => i.Tags));
        }
    }
}
