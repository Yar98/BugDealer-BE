using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.BtsException
{
    public class CreatorCannotDeleteLeaderRole : Exception
    {
        public CreatorCannotDeleteLeaderRole()
            : base("Creator cannot delete leader role")
        {
        }

        public CreatorCannotDeleteLeaderRole(string message)
            :base(message)
        { 
        }

        public CreatorCannotDeleteLeaderRole(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
