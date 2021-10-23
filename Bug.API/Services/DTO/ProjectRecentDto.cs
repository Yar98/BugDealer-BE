using System;
using System.Collections.Generic;

namespace Bug.API.Services.DTO
{
    public class ProjectRecentDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TotalIssues { get; set; }
        public int NumberDoneIssues { get; set; }
        public int NumberOpenIssues { get; set; }
    }
}