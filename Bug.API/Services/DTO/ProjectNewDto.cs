using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Services.DTO
{
    public class ProjectNewDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CreatorId { get; set; }
        public string WorkflowId { get; set; }
    }
}
