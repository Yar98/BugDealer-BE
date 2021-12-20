using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class RelationNormalDto
    {
        public string Description { get; set; }
        public int TagId { get; set; }
        public string FromIssueId { get; set; }
        public string ToIssueId { get; set; }
        public string ModifierId { get; set; }
    }
}
