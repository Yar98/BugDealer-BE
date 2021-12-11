using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class StatusesByProjectIdSearchSpecification : BaseSpecification<Status>
    {
        public StatusesByProjectIdSearchSpecification(string projectId, string search)
            : base(st=>st.Projects.AsQueryable().Any(p=>p.Id == projectId) &&
            st.Name.Contains(search))
        {
            AddInclude(s => s.Tag);
            AddInclude(st => st.Creator);
            AddInclude(st => st.Projects);
        }
    }
}
