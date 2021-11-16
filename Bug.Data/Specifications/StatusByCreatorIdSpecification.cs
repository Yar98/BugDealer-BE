using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class StatusByCreatorIdSpecification : BaseSpecification<Status>
    {
        public StatusByCreatorIdSpecification(string creatorId)
            : base(st=>st.Creator.Id == creatorId)
        {
            AddInclude(st => st.Creator);
            AddInclude(st => st.Projects);
            AddInclude(st => st.Tag);
        }
    }
}
