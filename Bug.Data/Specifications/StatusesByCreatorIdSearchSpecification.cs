using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class StatusesByCreatorIdSearchSpecification : BaseSpecification<Status>
    {
        public StatusesByCreatorIdSearchSpecification(string creatorId, string search)
            :base(st=>st.CreatorId == creatorId && st.Name.Contains(search))
        {
            AddInclude(s => s.Tag);
            AddInclude(st => st.Creator);
            AddInclude(st => st.Projects);
        }
    }
}
