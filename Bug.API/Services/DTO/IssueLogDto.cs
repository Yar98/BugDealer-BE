using System;

namespace Bug.API.Services.DTO
{
    public class IssuelogDto
    {
        public DateTime TimeLog { get; set; }
        public string IssueId { get; set; }
        public string ModifierId { get; set; }
        public string PreStatusId { get; set; }
        public string ModStatusId { get; set; }

    }
}