using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class ProjectsByCodeCreatorIdSpecification : BaseSpecification<Project>
    {
        public ProjectsByCodeCreatorIdSpecification(string creatorId, string code)
            : base(p=>p.CreatorId == creatorId && p.Code == code)
        {
            AddInclude(p => p.Creator);
            AddInclude(p => p.Template);
            AddInclude(p => p.DefaultAssignee);
            //AddInclude(p => p.Roles);
            //AddInclude(p => p.Issues);
            //AddInclude(p => p.Statuses);
            //AddInclude(a => a.AccountProjectRoles);
            //AddInclude("AccountProjectRoles.Project");
            //AddInclude("Issues.Tags");
            //AddInclude("Issues.State");
        }
    }
}
