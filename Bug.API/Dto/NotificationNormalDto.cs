using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class NotificationNormalDto
    {
        public int Id { get; set; }
        public bool Seen { get; set; }
        public int IssuelogId { get; set; }
        public string AccountId { get; set; }
    }
}
