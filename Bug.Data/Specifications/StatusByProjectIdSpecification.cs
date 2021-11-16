using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class StatusByProjectIdSpecification : BaseSpecification<Status>
    {
        public StatusByProjectIdSpecification(string projectId)
            : base(st => st.Projects.AsQueryable().Any(p=>p.Id == projectId))
        {
            AddInclude(st => st.Creator);
            AddInclude(st => st.Projects);
            AddInclude(st => st.Tag);
        }
    }
}
