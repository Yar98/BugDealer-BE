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
        public string LogDate { get; set; }
        public string CreatedDate { get; set; }
        public string DueDate { get; set; }
        public string OriginEstimateTime { get; set; }
        public string RemainEstimateTime { get; set; }
        public string Environment { get; set; }
        public string StatusId { get; set; }
        public string PriorityId { get; set; }
        public string SeverityId { get; set; }
        public string ProjectId { get; set; }
        public string ReporterId { get; set; }
        public string AssigneeId { get; set; }
        public List<TagNormalDto> Tags { get; set; }
        public List<AttachmentNormalDto> Attachments { get; set; }
        public List<RelationNormalDto> FromRelations { get; set; }
        public List<RelationNormalDto> ToRelations { get; set; }

        public string ModifierId { get; set; }
        public string LogDescription { get; set; }
        
    }
}
