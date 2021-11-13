using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class StatusByCreatorSpecification : BaseSpecification<Status>
    {
        public StatusByCreatorSpecification(string creatorId)
            : base(st=>st.Creator.Id == creatorId)
        {
            AddInclude(st => st.Creator);
            AddInclude(st => st.Projects);
        }
    }
}
