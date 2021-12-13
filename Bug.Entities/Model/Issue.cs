﻿using Bug.Entities.Integration;
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
        public DateTimeOffset? LogDate { get; private set; }
        public DateTimeOffset? CreatedDate { get; private set; }
        public DateTimeOffset? DueDate { get; private set; }
        public DateTimeOffset? WorklogDate { get; private set; }
        public string OriginEstimateTime { get; private set; }
        public string RemainEstimateTime { get; private set; }
        public string Environment { get; private set; }
        public string StatusId { get; private set; }
        public Status Status { get; private set; }
        public int? SeverityId { get; private set; }
        public Severity Severity { get; private set; }
        public int? PriorityId { get; private set; }
        public Priority Priority { get; private set; }
        public string ProjectId { get; private set; }
        public Project Project { get; private set; }
        public string ReporterId { get; private set; }
        public Account Reporter { get; private set; }
        public string AssigneeId { get; private set; }
        public Account Assignee { get; private set; }
        public int? WorklogId { get; private set; }
        public Worklog Worklog { get; private set; }

        public ICollection<Account> Watcher { get; private set; }
        public ICollection<Account> Voter { get; private set; }

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
            string assigneeId,
            int? worklogId)
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
            WorklogId = worklogId;
        }

        public void UpdateWorklogId(int i)
        {
            WorklogId = i;
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
        public void UpdatePriorityId(int? i)
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
        public void UpdateDueDate(DateTimeOffset? dt)
        {
            DueDate = dt;
        }
        public void UpdateAssigneeId(string id)
        {
            AssigneeId = id;
        }
        public void UpdateLogDate(DateTimeOffset? logDate)
        {
            LogDate = logDate;
        }
        public void UpdateCreatedDate(DateTimeOffset? dt)
        {
            CreatedDate = dt;
        }
        public void UpdateEnvironment(string e)
        {
            Environment = e;
        }
        public void UpdateStatusId(string st)
        {
            StatusId = st;
        }
        public void UpdateWorklogDate(DateTimeOffset? dt)
        {
            WorklogDate = dt;
        }
        public void UpdateSeverityId(int? id)
        {
            SeverityId = id;
        }
        
        public void AddWorklog(string spentTime, string remainTime, DateTimeOffset logDate, string loggerId)
        {
            Worklog = new(0, spentTime, remainTime, logDate, loggerId);
        }

        public void UpdateAttachments(List<Attachment> result)
        {
            if(result != null && result.Any())
                _attachments = result;
        }

        public void UpdateTags(List<Tag> result)
        {
            if (result != null && result.Any())
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
