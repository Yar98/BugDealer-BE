using Bug.Entities.Builder;
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
        
        // relations by fromIssueId
        private List<Relation> _fromRelations = new();
        public ICollection<Relation> FromRelations => _fromRelations.AsReadOnly();

        // relations by toIssueId
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
            set => Code = value;
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

        public void UpdateTitle(string title, string modifierId, Action<Issuelog> temp)
        {
            if (title == null)
                return;
            if (title == "")
                title = null;                        
            var log = new IssuelogBuilder()
                    .AddIssueId(Id)
                    .AddModifierId(modifierId)
                    .AddTagId(1)
                    .AddLogDate()
                    .AddOldTitle(Title)
                    .AddNewTitle(title)
                    .Build();
            temp.Invoke(log);
            Title = title;
        }

        public void UpdateDescription(string des, string modifierId, Action<Issuelog> temp)
        {
            if (des == null)
                return;
            if (des == "")
                des = null;
            var log = new IssuelogBuilder()
                    .AddIssueId(Id)
                    .AddModifierId(modifierId)
                    .AddTagId(1)
                    .AddLogDate()
                    .AddOldDescription(Description)
                    .AddNewDescription(des)
                    .Build();
            temp.Invoke(log);
            Description = des;
        }
        public void UpdateReporterId(string id, string modifierId, Action<Issuelog> temp)
        {
            if (id == null)
                return;
            if (id == "")
                id = null;
            var log = new IssuelogBuilder()
                    .AddIssueId(Id)
                    .AddModifierId(modifierId)
                    .AddTagId(1)
                    .AddLogDate()
                    .AddOldReporterId(ReporterId)
                    .AddNewReporterId(id)
                    .Build();
            temp.Invoke(log);
            ReporterId = id;
        }
        public void UpdatePriorityId(string i, string modifierId, Action<Issuelog> temp)
        {
            if (i == null)
                return;
            if (i == "")
                PriorityId = null;
            var log = new IssuelogBuilder()
                .AddIssueId(Id)
                .AddModifierId(modifierId)
                .AddTagId(1)
                .AddLogDate()
                .AddOldPriorityId(PriorityId ?? 1)
                .AddNewPriorityId(int.Parse(i))
                .Build();
            temp.Invoke(log);
            PriorityId = int.Parse(i);
        }
        public void UpdateOriginalEstimateTime(string s, string modifierId, Action<Issuelog> temp)
        {
            if (s == null)
                return;
            if (s == "")
                OriginEstimateTime = null;
            var log = new IssuelogBuilder()
                    .AddIssueId(Id)
                    .AddModifierId(modifierId)
                    .AddTagId(1)
                    .AddLogDate()
                    .AddOldOriginEstimateTime(OriginEstimateTime)
                    .AddNewOriginEstimateTime(s)
                    .Build();
            temp.Invoke(log);
            OriginEstimateTime = s;

        }
        public void UpdateRemainEstimateTime(string s, string modifierId, Action<Issuelog> temp)
        {
            if (s == null)
                return;
            if (s == "")
                s = null;
            var log = new IssuelogBuilder()
                    .AddIssueId(Id)
                    .AddModifierId(modifierId)
                    .AddTagId(1)
                    .AddLogDate()
                    .AddOldRemainEstimateTime(RemainEstimateTime)
                    .AddNewRemainEstimateTime(s)
                    .Build();
            temp.Invoke(log);
            RemainEstimateTime = s;
        }
        public void UpdateDueDate(string dt, string modifierId, Action<Issuelog> temp)
        {
            if (dt == null)
                return;
            if (dt == "")
                dt = null;           
            var log = new IssuelogBuilder()
                .AddIssueId(Id)
                .AddModifierId(modifierId)
                .AddTagId(1)
                .AddLogDate()
                .AddOldDueDate(DueDate)
                .AddNewDueDate(DateTimeOffset.Parse(dt))
                .Build();
            temp.Invoke(log);
            DueDate = DateTimeOffset.Parse(dt);

        }
        public void UpdateAssigneeId(string id, string modifierId, Action<Issuelog> temp)
        {
            if (id == null)
                return;
            if (id == "")
                id = null;
            var log = new IssuelogBuilder()
                    .AddIssueId(Id)
                    .AddModifierId(modifierId)
                    .AddTagId(1)
                    .AddLogDate()
                    .AddOldAssigneeId(AssigneeId)
                    .AddNewAssigneeId(id)
                    .Build();
            temp.Invoke(log);
            AssigneeId = id;
        }
        public void UpdateEnvironment(string e, string modifierId, Action<Issuelog> temp)
        {
            if (e == null)
                return;
            if (e == "")
                e = null;            
            var log = new IssuelogBuilder()
                    .AddIssueId(Id)
                    .AddModifierId(modifierId)
                    .AddTagId(1)
                    .AddLogDate()
                    .AddOldEnvironment(Environment)
                    .AddNewEnvironment(e)
                    .Build();
            temp.Invoke(log);
            Environment = e;
        }
        public void UpdateStatusId(Status newStatus, string modifierId, Action<Issuelog> temp)
        {
            if (newStatus == null)
                return;
            var log = new IssuelogBuilder()
                    .AddIssueId(Id)
                    .AddModifierId(modifierId)
                    .AddTagId(1)
                    .AddLogDate()
                    .AddOldStatusTagId(Status.TagId)
                    .AddOldStatusName(Status.Name)
                    .AddNewStatusTagId(newStatus.TagId)
                    .AddNewStatusName(newStatus.Name)
                    .Build();
            temp.Invoke(log);
            StatusId = newStatus.Id;
        }
        public void UpdateStatusId(string newStatus)
        {            
            StatusId = newStatus;
        }
        public void UpdateSeverityId(string id, string modifierId, Action<Issuelog> temp)
        {
            if (id == "")            
                id = null;
            if (id == null)
                return;
            var log = new IssuelogBuilder()
                    .AddIssueId(Id)
                    .AddModifierId(modifierId)
                    .AddTagId(1)
                    .AddLogDate()
                    .AddOldSeverityId(SeverityId??1)
                    .AddNewSeverityId(int.Parse(id))
                    .Build();
            temp.Invoke(log);
            SeverityId = int.Parse(id);
        }

        public void UpdateAttachments(List<Attachment> result)
        {
            if(result != null)
                _attachments = result;
        }

        public void UpdateTags(List<Tag> result)
        {
            if (result != null)
            {
                _tags.Clear();
                _tags = result;
            }                
        }

        public void UpdateFromRelations(List<Relation> result)
        {
            if (result != null && result.Any())
                _fromRelations = result;
        }

        public void UpdateStatus(Status st)
        {
            Status = st;
        }
    }
}
