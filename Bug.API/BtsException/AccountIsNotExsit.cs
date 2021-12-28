using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.BtsException
{
    public class AccountIsNotExsit : Exception
    {
        public AccountIsNotExsit()
            : base("Account not exist")
        {
        }

        public AccountIsNotExsit(string message)
            : base(message)
        {
        }

        public AccountIsNotExsit(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
