using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.Entities.ModelException
{
    public class DueDateOfIssueMustWithinDueDateOfProject : Exception
    {
        public DueDateOfIssueMustWithinDueDateOfProject()
            : base("DueDate Of Issue Must Within DueDate Of Project")
        {
        }

        public DueDateOfIssueMustWithinDueDateOfProject(string message)
            : base(message)
        {
        }

        public DueDateOfIssueMustWithinDueDateOfProject(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
