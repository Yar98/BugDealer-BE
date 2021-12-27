using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.BtsException
{
    public class OnlyAssigneeAddWorklog : Exception
    {
        public OnlyAssigneeAddWorklog()
            : base("Only assignee can add worklog")
        {
        }

        public OnlyAssigneeAddWorklog(string message)
            : base(message)
        {
        }

        public OnlyAssigneeAddWorklog(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
