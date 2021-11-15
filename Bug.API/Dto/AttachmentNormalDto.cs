using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class AttachmentNormalDto
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public string IssueId { get; set; }
    }
}
