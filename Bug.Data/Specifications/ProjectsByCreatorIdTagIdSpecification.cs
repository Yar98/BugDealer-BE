using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class ProjectsByCreatorIdTagIdSpecification : BaseSpecification<Project>
    {
        public ProjectsByCreatorIdTagIdSpecification(string accountId)
            : base(p=>p.CreatorId == accountId)
        {
            AddInclude(p => p.Issues.SelectMany(i => i.Tags));
        }
        public ProjectsByCreatorIdTagIdSpecification
            (string accountId,
            int tagId)
            : base(p => p.CreatorId == accountId && 
            p.Tags.AsQueryable().Any(t=>t.Id == tagId))
        {
            AddInclude(p => p.Creator);
            AddInclude(p => p.DefaultAssignee);
            AddInclude(p => p.Accounts);
            AddInclude(p => p.Roles);
            AddInclude(p => p.Issues);
            AddInclude(p => p.Tags);
            AddInclude("Issues.Tags");
        }
    }
}
