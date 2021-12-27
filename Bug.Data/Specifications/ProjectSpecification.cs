using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class ProjectSpecification : BaseSpecification<Project>
    {
        public ProjectSpecification(string projectId)
            : base(p=>p.Id == projectId)
        {
            AddInclude(p => p.Creator);
            AddInclude(p => p.DefaultAssignee);
            AddInclude(p => p.Template);
            AddInclude(p => p.Roles);
            //AddInclude(p => p.Issues);
            AddInclude(p => p.Statuses);
            //AddInclude(a => a.AccountProjectRoles);
            //AddInclude("Issues.Tags");
            //AddInclude("Issues.State");
        }

        public ProjectSpecification(string projectId, int count)
            : base(p => p.Id == projectId)
        {
            if (count == 1)
                AddInclude(p => p.Issues);
        }
    }
}
