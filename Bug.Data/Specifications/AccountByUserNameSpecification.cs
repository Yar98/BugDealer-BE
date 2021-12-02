using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class AccountByUserNameSpecification : BaseSpecification<Account>
    {
        public AccountByUserNameSpecification(string username)
            :base(a=>a.UserName == username)
        {
            AddInclude(a => a.Timezone);
        }
    }
}
