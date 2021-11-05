using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Issue : IEntityBase, IIntegrationBase
    {
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime LogDate { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public string OriginEstimateTime { get; private set; }
        public string RemainEstimateTime { get; private set; }
        public string Environment { get; private set; }
        public string StatusId { get; private set; }
        public Status Status { get; private set; }
        public int PriorityId { get; private set; }
        public Priority Priority { get; private set; }
        public string ProjectId { get; private set; }
        public Project Project { get; private set; }
        public string ReporterId { get; private set; }
        public Account Reporter { get; private set; }
        public string AssigneeId { get; private set; }
        public Account Assignee { get; private set; }

        private readonly List<Tag> _tags = new List<Tag>();
        public ICollection<Tag> Tags => _tags.AsReadOnly();

        private readonly List<Attachment> _attachments = new List<Attachment>();
        public ICollection<Attachment> Attachments => _attachments.AsReadOnly();

        private Issue() { }
        //[JsonConstructor]
        public Issue(string id,
            string title,
            string description,
            DateTime timeLog,
            DateTime createdDate,
            DateTime dueDate,
            string originEstimateTime,
            string remainEstimateTime,
            string environment,
            string statusId,
            int priorityId,
            string projectId,
            string reporterId,
            string assigneeId)
        {
            Id = id;
            Title = title;
            Description = description;
            LogDate = timeLog;
            CreatedDate = createdDate;
            DueDate = dueDate;
            OriginEstimateTime = originEstimateTime;
            RemainEstimateTime = remainEstimateTime;
            Environment = environment;
            StatusId = statusId;
            PriorityId = priorityId;
            ProjectId = projectId;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
        }

        public void UpdateProjectId(string id)
        {
            ProjectId = id;
        }
        public void UpdateTitle(string title)
        {
            Title = title;
        }
        public void UpdateDescription(string des)
        {
            Description = des;
        }
        public void UpdateReporterId(string id)
        {
            ReporterId = id;
        }
        public void UpdatePriorityId(int i)
        {
            PriorityId = i;
        }
        public void UpdateOriginalEstimateTime(string s)
        {
            OriginEstimateTime = s;
        }
        public void UpdateRemainEstimateTime(string s)
        {
            RemainEstimateTime = s;
        }
        public void UpdateDueDate(DateTime dt)
        {
            DueDate = dt;
        }
        public void UpdateAssigneeId(string id)
        {
            AssigneeId = id;
        }

        public void UpdateAttachments(IEnumerable<Attachment> result)
        {

        }
        public void UpdateTags(IEnumerable<Tag> result)
        {
            _tags.Clear();
            _tags.AddRange(result);
        }
    }
}
