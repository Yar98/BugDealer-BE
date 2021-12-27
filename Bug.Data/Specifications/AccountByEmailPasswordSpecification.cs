using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    class AccountByEmailPasswordSpecification : BaseSpecification<Account>
    {
        public AccountByEmailPasswordSpecification(string email, string password)
            : base(a=>a.Email == email && a.Password == password)
        {

        }
    }
}
