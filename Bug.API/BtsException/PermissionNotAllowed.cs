using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.BtsException
{
    public class PermissionNotAllowed : Exception
    {
        public PermissionNotAllowed()
            : base("Dont have permission to do this")
        {
        }

        public PermissionNotAllowed(string message)
            : base(message)
        {
        }

        public PermissionNotAllowed(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
