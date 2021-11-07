using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class AccountsByProjectSpecification : BaseSpecification<Account>
    {
        public AccountsByProjectSpecification(string projectId)
            : base(m=>m.Id != null && 
            m.Projects.AsQueryable().Any(p=>p.Id==projectId))
        {
            AddInclude(m => m.Projects);
            
        }
    }
}
