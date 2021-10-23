using System;

namespace Bug.API.Services.DTO
{
    public class CommentDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeLog { get; set; }
        public string IssueId { get; set; }
        public string AccountId { get; set; }
    }
}