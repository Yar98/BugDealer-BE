using Bug.Entities.Integration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Issue : IEntityBase
    {
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int NumberCode { get; private set; }
        public DateTimeOffset? LogDate { get; private set; } //ko can
        public DateTimeOffset? CreatedDate { get; private set; }
        public DateTimeOffset? DueDate { get; private set; }
        public DateTimeOffset? WorklogDate { get; private set; } //ko can
        public string OriginEstimateTime { get; private set; }
        public string RemainEstimateTime { get; private set; }
        public string Environment { get; private set; }
        public string? StatusId { get; private set; }
        public Status Status { get; private set; }
        public int? SeverityId { get; private set; }
        public Severity Severity { get; private set; }
        public int? PriorityId { get; private set; }
        public Priority Priority { get; private set; }
        public string ProjectId { get; private set; }
        public Project Project { get; private set; }
        public string ReporterId { get; private set; }
        public Account Reporter { get; private set; }
        public string? AssigneeId { get; private set; }
        public Account Assignee { get; private set; }

        public ICollection<Account> Watchers { get; private set; }
        public ICollection<Account> Voters { get; private set; }

        private List<Relation> _fromRelations = new();
        public ICollection<Relation> FromRelations => _fromRelations.AsReadOnly();

        private List<Relation> _toRelations = new();
        public ICollection<Relation> ToRelations => _toRelations.AsReadOnly();

        public List<RelatedIssues> LinkedIssues
        {
            get
            {
                return FromRelations.GroupBy(r => r.Tag)
                    .Select(gr=>new RelatedIssues 
                    {
                        Tag = gr.Key,
                        Issues = gr.Select(item=>item.ToIssue).ToList()
                    })
                    .ToList();
            }
        }

        private List<Tag> _tags = new();
        public ICollection<Tag> Tags => _tags.AsReadOnly();

        private List<Attachment> _attachments = new();
        public ICollection<Attachment> Attachments => _attachments.AsReadOnly();

        public string Code
        {
            get
            {
                return Project?.Code + "-" + NumberCode;
            }
        }

        private Issue() { }
        //[JsonConstructor]
        public Issue
            (string id,
            string title,
            int numberCode,
            string description,
            DateTimeOffset? timeLog,
            DateTimeOffset? createdDate,
            DateTimeOffset? dueDate,
            DateTimeOffset? worklogDate,
            string originEstimateTime,
            string remainEstimateTime,
            string environment,
            string statusId,
            int? priorityId,
            int? severityId,
            string projectId,
            string reporterId,
            string assigneeId)
        {
            Id = id;
            Title = title;
            NumberCode = numberCode;
            Description = description;
            LogDate = timeLog;
            CreatedDate = createdDate;
            DueDate = dueDate;
            WorklogDate = worklogDate;
            OriginEstimateTime = originEstimateTime;
            RemainEstimateTime = remainEstimateTime;
            Environment = environment;
            StatusId = statusId;
            PriorityId = priorityId;
            SeverityId = severityId;
            ProjectId = projectId;
            ReporterId = reporterId;
            AssigneeId = assigneeId;
        }

        public void UpdateTitle(string title)
        {
            if(title == "")
                Title = null;
            Title = title ?? Title;
        }
        public void UpdateDescription(string des)
        {
            if (des == "")
                Description = null;
            Description = des??Description;
        }
        public void UpdateReporterId(string id)
        {
            if(id == "")
                ReporterId = null;
            ReporterId = id ?? ReporterId;
        }
        public void UpdatePriorityId(string i)
        {
            if(i == "")            
                PriorityId = null;
            else if(i != null)            
                PriorityId = int.Parse(i);
                     
        }
        public void UpdateOriginalEstimateTime(string s)
        {
            if (s == "")
                OriginEstimateTime = null;
            OriginEstimateTime = s??OriginEstimateTime;
        }
        public void UpdateRemainEstimateTime(string s)
        {
            if (s == "")
                RemainEstimateTime = null;
            RemainEstimateTime = s?? RemainEstimateTime;
        }
        public void UpdateDueDate(string dt)
        {
            if (dt == "")
            {
                DueDate = null;
            }else if(dt != null)
            {
                DueDate = DateTimeOffset.Parse(dt);
            }          
        }
        public void UpdateAssigneeId(string id)
        {
            if (id == "")
                AssigneeId = null;
            else if (id != null)
                AssigneeId = id;
        }
        public void UpdateCreatedDate(string dt)
        {
            if(dt == "")
            {
                CreatedDate = null;
            }else if(dt != null)
            {
                CreatedDate = DateTimeOffset.Parse(dt);
            }           
        }
        public void UpdateEnvironment(string e)
        {
            if (e == "")
                Environment = null;
            Environment = e ?? Environment;
        }
        public void UpdateStatusId(string st)
        {
            StatusId = st;
        }
        public void UpdateSeverityId(string id)
        {
            if (id == "")
            {
                SeverityId = null;
            }else if(id != null)
            {
                SeverityId = int.Parse(id);
            }
        }

        public void UpdateAttachments(List<Attachment> result)
        {
            if(result != null)
                _attachments = result;
        }

        public void UpdateTags(List<Tag> result)
        {
            if (result != null)
                _tags = result;            
        }

        public void UpdateFromRelations(List<Relation> result)
        {
            if (result != null && result.Any())
                _fromRelations = result;
        }

        public void UpdateToRelations(List<Relation> result)
        {
            if (result != null && result.Any())
                _toRelations = result;
        }

        public void UpdateStatus(Status st)
        {
            Status = st;
        }
    }
}
