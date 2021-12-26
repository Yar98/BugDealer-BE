using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.BtsException
{
    public class CannotDeleteStatusInUse : Exception
    {
        public CannotDeleteStatusInUse()
            : base("Can not delete status in use")
        {
        }

        public CannotDeleteStatusInUse(string message)
            : base(message)
        {
        }

        public CannotDeleteStatusInUse(string message, Exception inner)
            :base(message, inner)
        {

        }
    }
}
