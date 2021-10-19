using System;
using System.Collections.Generic;

namespace Bug.Entities.Dtos
{
    public class ProjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string CreatorId { get; set; }
        public string WorkflowId { get; set; }

    }
}