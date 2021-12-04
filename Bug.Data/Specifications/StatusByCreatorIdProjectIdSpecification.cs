using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class StatusByCreatorIdProjectIdSpecification : BaseSpecification<Status>
    {
        public StatusByCreatorIdProjectIdSpecification(string projectId, string creatorId)
            : base(st => st.Creator.Id == creatorId && 
            st.Projects.AsQueryable().Any(p=>p.Id == projectId))
        {
            AddInclude(st => st.Creator);
            AddInclude(st => st.Projects);
            AddInclude(st => st.Tag);
        }
    }
}
