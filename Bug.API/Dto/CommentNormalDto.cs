using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class CommentNormalDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset TimeLog { get; set; }
        public string IssueId { get; set; }
        public string AccountId { get; set; }
    }
}
