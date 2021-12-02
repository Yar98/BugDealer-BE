using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.API.Dto
{
    public class IssuePostDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? LogDate { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public DateTimeOffset? WorklogDate { get; set; }
        public string OriginEstimateTime { get; set; }
        public string RemainEstimateTime { get; set; }
        public string Environment { get; set; }
        public string StatusId { get; set; }
        public int PriorityId { get; set; }
        public string ProjectId { get; set; }
        public string ReporterId { get; set; }
        public string AssigneeId { get; set; }
        public int WorklogId { get; set; }
        public List<TagNormalDto> Tags { get; set; }
        public List<AttachmentNormalDto> Attachments { get; set; }
        public List<RelationNormalDto> FromRelations { get; set; }
        public List<RelationNormalDto> ToRelations { get; set; }
        
    }
}
