using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.BtsException
{
    public class NotJoinThisProject : Exception
    {
        public NotJoinThisProject()
            : base("Not join this project")
        {
        }

        public NotJoinThisProject(string message)
            : base(message) { }

        public NotJoinThisProject(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
