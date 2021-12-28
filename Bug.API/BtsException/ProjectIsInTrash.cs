using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.BtsException
{
    public class ProjectIsInTrash : Exception
    {
        public ProjectIsInTrash()
            : base("This project is in trash")
        {
        }

        public ProjectIsInTrash(string message)
            : base(message)
        {
        }

        public ProjectIsInTrash(string message, Exception inner)
            : base(message, inner) 
        { 
        }
    }
}
