using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.BtsException
{
    public class ExistEmailInBts : Exception
    {
        public ExistEmailInBts()
            : base("This email is exsit in bts")
        {
        }

        public ExistEmailInBts(string message)
            : base(message)
        {
        }

        public ExistEmailInBts(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
