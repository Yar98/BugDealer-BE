using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class ProjectsBySearchWhichMemberIdJoinSpecification : BaseSpecification<Project>
    {
        public ProjectsBySearchWhichMemberIdJoinSpecification(string memberId, int state, string search)
            :base(p=>p.AccountProjectRoles.AsQueryable().Any(apr=>apr.AccountId == memberId) &&
            p.State == state &&
            (p.Name.Contains(search) || p.Description.Contains(search) || p.Code.Contains(search) || p.Template.Name.Contains(search)))
        {
            AddInclude(p => p.Creator);
            AddInclude(p => p.Template);
            AddInclude(p => p.DefaultAssignee);
            //AddInclude(p => p.Roles);
            //AddInclude(p => p.Issues);
            //AddInclude(p => p.Statuses);
            //AddInclude(a => a.AccountProjectRoles);
            //AddInclude("Issues.Tags");
            //AddInclude("Issues.State");
        }
    }
}
