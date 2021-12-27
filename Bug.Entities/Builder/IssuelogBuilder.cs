using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Builder
{
    public class IssuelogBuilder : IIssuelogBuilder
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public string IssueId { get; private set; }
        public Issue Issue { get; private set; }
        public string ModifierId { get; private set; }
        public Account Modifier { get; private set; }
        // old status
        public int? OldStatusTagId { get; private set; }
        public Tag OldStatusTag { get; private set; }
        public string OldStatusName { get; private set; }
        // new status
        public int? NewStatusTagId { get; private set; }
        public string NewStatusName { get; private set; }
        public Tag NewStatusTag { get; private set; }
        // old priority
        public int? OldPriorityId { get; private set; }
        public Priority OldPriority { get; private set; }
        // new priority
        public int? NewPriorityId { get; private set; }
        public Priority NewPriority { get; private set; }
        // old severity
        public int? OldSeverityId { get; private set; }
        public Severity OldSeverity { get; private set; }
        // new severity
        public int? NewSeverityId { get; private set; }
        public Severity NewSeverity { get; private set; }
        // old assignee
        public string? OldAssigneeId { get; private set; }
        public Account OldAssignee { get; private set; }
        // new assignee
        public string? NewAssigneeId { get; private set; }
        public Account NewAssignee { get; private set; }
        // old reporter
        public string? OldReporterId { get; private set; }
        public Account OldReporter { get; private set; }
        // new reporter
        public string? NewReporterId { get; private set; }
        public Account NewReporter { get; private set; }
        // old worklogId
        public int? OldWorklogId { get; private set; }
        public Worklog OldWorklog { get; private set; }
        // new worklogId
        public int? NewWorklogId { get; private set; }
        public Worklog NewWorklog { get; private set; }
        // old description
        public string OldDescription { get; private set; }
        // new description
        public string NewDescription { get; private set; }
        // old title
        public string OldTitle { get; private set; }
        // new title
        public string NewTitle { get; private set; }
        // old origin estimate time
        public string OldOriginEstimateTime { get; private set; }
        // new origin estimate time
        public string NewOriginEstimateTime { get; private set; }
        // old remain estimate time
        public string OldRemainEstimateTime { get; private set; }
        // new remain estimate time
        public string NewRemainEstimateTime { get; private set; }
        // old due date
        public DateTimeOffset? OldDueDate { get; private set; }
        // new due date
        public DateTimeOffset? NewDueDate { get; private set; }
        // old environment
        public string OldEnvironment { get; private set; }
        // new environment
        public string NewEnvironment { get; private set; }
        // old toIssueId
        public string OldToIssueId { get; private set; }
        // new toIssueId
        public string NewToIssueId { get; private set; }

        // action
        public int TagId { get; private set; }

        public IIssuelogBuilder AddOldToIssueId(string id)
        {
            OldToIssueId = id;
            return this;
        }

        public IIssuelogBuilder AddNewToIssueId(string id)
        {
            NewToIssueId = id;
            return this;
        }

        public IIssuelogBuilder AddDescription(string des)
        {
            Description = des;
            return this;
        }

        public IIssuelogBuilder AddId()
        {
            Id = 0;
            return this;
        }

        public IIssuelogBuilder AddIssueId(string issueId)
        {
            IssueId = issueId;
            return this;
        }

        public IIssuelogBuilder AddModifierId(string accountId)
        {
            ModifierId = accountId;
            return this;
        }

        public IIssuelogBuilder AddNewAssigneeId(string id)
        {
            NewAssigneeId = id;
            return this;
        }

        public IIssuelogBuilder AddNewDescription(string des)
        {
            NewDescription = des;
            return this;
        }

        public IIssuelogBuilder AddNewDueDate(DateTimeOffset? dt)
        {
            NewDueDate = dt;
            return this;
        }

        public IIssuelogBuilder AddNewEnvironment(string dt)
        {
            NewEnvironment = dt;
            return this;
        }

        public IIssuelogBuilder AddNewOriginEstimateTime(string time)
        {
            NewOriginEstimateTime = time;
            return this;
        }

        public IIssuelogBuilder AddNewPriorityId(int? priorityId)
        {
            NewPriorityId = priorityId;
            return this;
        }

        public IIssuelogBuilder AddNewRemainEstimateTime(string time)
        {
            NewRemainEstimateTime = time;
            return this;
        }

        public IIssuelogBuilder AddNewReporterId(string id)
        {
            NewReporterId = id;
            return this;
        }

        public IIssuelogBuilder AddNewSeverityId(int? id)
        {
            NewSeverityId = id;
            return this;
        }

        public IIssuelogBuilder AddNewStatusName(string statusId)
        {
            NewStatusName = statusId;
            return this;
        }

        public IIssuelogBuilder AddNewStatusTagId(int? id)
        {
            NewStatusTagId = id;
            return this;
        }

        public IIssuelogBuilder AddNewTitle(string title)
        {
            NewTitle = title;
            return this;
        }

        public IIssuelogBuilder AddNewWorklogId(int? id)
        {
            NewWorklogId = id;
            return this;
        }

        public IIssuelogBuilder AddOldAssigneeId(string id)
        {
            OldAssigneeId = id;
            return this;
        }

        public IIssuelogBuilder AddOldDescription(string des)
        {
            OldDescription = des;
            return this;
        }

        public IIssuelogBuilder AddOldDueDate(DateTimeOffset? dt)
        {
            OldDueDate = dt;
            return this;
        }

        public IIssuelogBuilder AddOldEnvironment(string dt)
        {
            OldEnvironment = dt;
            return this;
        }

        public IIssuelogBuilder AddOldOriginEstimateTime(string time)
        {
            OldOriginEstimateTime = time;
            return this;
        }

        public IIssuelogBuilder AddOldPriorityId(int? priorityId)
        {
            OldPriorityId = priorityId;
            return this;
        }

        public IIssuelogBuilder AddOldRemainEstimateTime(string time)
        {
            OldRemainEstimateTime = time;
            return this;
        }

        public IIssuelogBuilder AddOldReporterId(string id)
        {
            OldReporterId = id;
            return this;
        }

        public IIssuelogBuilder AddOldSeverityId(int? id)
        {
            OldSeverityId = id;
            return this;
        }

        public IIssuelogBuilder AddOldStatusName(string statusId)
        {
            OldStatusName = statusId;
            return this;
        }

        public IIssuelogBuilder AddOldStatusTagId(int? id)
        {
            OldStatusTagId = id;
            return this;
        }

        public IIssuelogBuilder AddOldTitle(string title)
        {
            OldTitle = title;
            return this;
        }

        public IIssuelogBuilder AddOldWorklogId(int? id)
        {
            OldWorklogId = id;
            return this;
        }

        public IIssuelogBuilder AddTagId(int tagId)
        {
            TagId = tagId;
            return this;
        }

        public Issuelog Build()
        {
            return new Issuelog
                (Id,
                Description,
                IssueId,
                ModifierId,
                OldStatusTagId,
                OldStatusName,
                NewStatusTagId,
                NewStatusName,
                OldPriorityId,
                NewPriorityId,
                OldSeverityId,
                NewSeverityId,
                OldAssigneeId,
                NewAssigneeId,
                OldReporterId,
                NewReporterId,
                OldWorklogId,
                NewWorklogId,
                OldDescription,
                NewDescription,
                OldTitle,
                NewTitle,
                OldOriginEstimateTime,
                NewOriginEstimateTime,
                OldRemainEstimateTime,
                NewRemainEstimateTime,
                OldDueDate,
                NewDueDate,
                OldEnvironment,
                NewEnvironment,
                OldToIssueId,
                NewToIssueId,
                TagId);
        }
    }
}