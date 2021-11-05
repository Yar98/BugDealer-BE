using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class ProjectDetailLv1Specification : BaseSpecification<Project>
    {
        public ProjectDetailLv1Specification(string projectId)
            : base(p=>p.Id == projectId)
        {
            AddInclude(p => p.Creator);
            //AddInclude(p => p.Workflow);
            AddInclude(p => p.DefaultAssignee);
            AddInclude(p => p.Accounts);
            AddInclude(p => p.Roles);
            AddInclude(p => p.Issues);
            AddInclude(p => p.Tags);
        }
    }
}
