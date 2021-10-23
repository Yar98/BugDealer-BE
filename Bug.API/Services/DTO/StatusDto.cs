using System.Collections.Generic;

namespace Bug.API.Services.DTO
{
    public class StatusDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public int TagId { get; set; }
        public string AccountId { get; set; }

    }
}