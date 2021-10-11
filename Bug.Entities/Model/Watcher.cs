using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Watcher
    {
        public string AccountId { get; private set; }
        public Account Account { get; private set; }
        public string IssueId { get; private set; }
        public Issue Issue { get; private set; }
    }
}
