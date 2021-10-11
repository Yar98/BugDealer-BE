using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Issue : IEntityBase
    {
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime TimeLog { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public string OriginEstimateTime { get; private set; }
        public string RemainEstimateTime { get; private set; }
        public string UriAttachment { get; private set; }
        public string Environment { get; private set; }
        public string LinkedIssueId { get; private set; }
        public int RelationId { get; private set; }
        public Relation Relation { get; private set; }
        public string LinkedStatusId { get; private set; }
        public Status LinkedStatus { get; private set; }
        public int PriorityId { get; private set; }
        public Priority Priority { get; private set; }
        public string ProjectId { get; private set; }
        public Project Project { get; private set; }
        public string ReporterId { get; private set; }
        public Account Reporter { get; private set; }
        public string AsigneeId { get; private set; }
        public Account Asignee { get; private set; }
        public ICollection<Tag> Tags { get; private set; }
        private Issue() { }
        public Issue(string id,
            string title,
            string description,
            DateTime timeLog,
            DateTime createdDate,
            DateTime dueDate,
            string originEstimateTime,
            string remainEstimateTime,
            string uriAttachment,
            string environment,
            string linkedStatusId,
            int priorityId,
            string projectId,
            string reporterId,
            string asigneeId,
            string linkedIssueId,
            int relationId)
        {
            Id = id;
            Title = title;
            Description = description;
            TimeLog = timeLog;
            CreatedDate = createdDate;
            DueDate = dueDate;
            OriginEstimateTime = originEstimateTime;
            RemainEstimateTime = remainEstimateTime;
            UriAttachment = uriAttachment;
            Environment = environment;
            LinkedStatusId = linkedStatusId;
            PriorityId = priorityId;
            ProjectId = projectId;
            ReporterId = reporterId;
            AsigneeId = asigneeId;
            LinkedIssueId = linkedIssueId;
            RelationId = relationId;
        }
    }
}
