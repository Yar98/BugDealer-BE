﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Services.DTO
{
    public class ProjectDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ProjectType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RecentDate { get; set; }
        public string Description { get; set; }
        public string AvatarUri { get; set; }
        public string DefaultAssigneeId { get; set; }
        public string CreatorId { get; set; }
        public string WorkflowId { get; set; }
    }
}