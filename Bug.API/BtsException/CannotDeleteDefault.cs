using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.BtsException
{
    public class CannotDeleteDefault : Exception
    {
        public CannotDeleteDefault()
            : base("Cannot Delete Default")
        {
        }

        public CannotDeleteDefault(string message)
            : base(message)
        {
        }

        public CannotDeleteDefault(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
