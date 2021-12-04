using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.BtsException
{
    public class OldPasswordWrong : Exception
    {
        public OldPasswordWrong()
            : base("Cannot Delete Default")
        {
        }

        public OldPasswordWrong(string message)
            : base(message)
        {
        }

        public OldPasswordWrong(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
