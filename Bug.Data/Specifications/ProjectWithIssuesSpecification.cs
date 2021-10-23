using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    class ProjectWithIssuesSpecification : BaseSpecification<Project>
    {
        public ProjectWithIssuesSpecification(string projectId)
            : base(p=>p.Id == projectId)
        {
            AddInclude(p => p.Issues);
        }
    }
}
