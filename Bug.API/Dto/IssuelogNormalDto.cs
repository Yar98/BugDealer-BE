using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class IssuelogNormalDto
    {
        public int Id { get; set; }
        public DateTimeOffset LogDate { get; set; }
        public string Description { get; set; }
        public string IssueId { get; set; }
        public string ModifierId { get; set; }
        public string PreStatusId { get; set; }
        public string ModStatusId { get; set; }
        public int PrePriorityId { get; set; }
        public int ModPriorityId { get; set; }
        public int TagId { get; set; }
    }
}
