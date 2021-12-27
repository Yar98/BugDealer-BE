using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.BtsException
{
    public class CannotDeleteRoleInUse : Exception
    {
        public CannotDeleteRoleInUse()
            : base("Can not delete role in use")
        {
        }

        public CannotDeleteRoleInUse(string message)
            : base(message)
        {
        }

        public CannotDeleteRoleInUse(string message, Exception inner)
            :base(message, inner)
        {

        }
    }
}
