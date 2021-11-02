using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class ProjectNormalSpecification : BaseSpecification<Project>
    {
        public ProjectNormalSpecification(string projectId)
            : base(p => p.Id == projectId)
        {
            AddInclude(p => p.Creator);
            AddInclude(p => p.Workflow);
            AddInclude(p => p.DefaultAssignee);
        }
    }
}
