using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.API.Dto
{
    public class IssueNormalDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime LogDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string OriginEstimateTime { get; set; }
        public string RemainEstimateTime { get; set; }
        public string Environment { get; set; }
        public string StatusId { get; set; }
        public string StatusName { get; set; }
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ReporterId { get; set; }
        public string ReporterName { get; set; }
        public string AssigneeId { get; set; }
        public string AssigneeName { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
