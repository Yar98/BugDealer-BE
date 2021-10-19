using System;
using System.Collections.Generic;

namespace Bug.Entities.Dtos
{
    public class IssueDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeLog { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string OriginEstimateTime { get; set; }
        public string RemainEstimateTime { get; set; }
        public string UriAttachment { get; set; }
        public string Environment { get; set; }
        public string LinkedIssueId { get; set; }
        public int RelationId { get; set; }
        public string LinkedStatusId { get; set; }
        public int PriorityId { get; set; }
        public string ProjectId { get; set; }
        public string ReporterId { get; set; }
        public string AssigneeId { get; set; }

    }
}