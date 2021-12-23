using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Issuelog : IEntityBase
    {
        public int Id { get; private set; }
        public DateTimeOffset LogDate { get; private set; }
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
        public int? OldOriginEstimateTime { get; private set; }
        // new origin estimate time
        public int? NewOriginEstimateTime { get; private set; }
        // old remain estimate time
        public int? OldRemainEstimateTime { get; private set; }
        // new remain estimate time
        public int? NewRemainEstimateTime { get; private set; }
        // old due date
        public DateTimeOffset? OldDueDate { get; private set; }
        // new due date
        public DateTimeOffset? NewDueDate { get; private set; }
        // old environment
        public string OldEnvironment { get; private set; }
        // new environment
        public string NewEnvironment { get; private set; }
        // old toIssue
        public string? OldToIssueId { get; private set; }
        public Issue OldToIssue { get; private set; }
        // new toIssue
        public string? NewToIssueId { get; private set; }
        public Issue NewToIssue { get; private set; }
        // action
        public int? TagId { get; private set; }
        public Tag Tag { get; private set; }

        private Issuelog() { }
        public Issuelog
            (int id,
            string des,
            string issueId,
            string modifierId,
            int? preStatusTagId,
            string preStatusName,
            int? modStatusTagId,
            string modStatusName,
            int? prePriorityId,
            int? modPriorityId,
            int? oldSeverityId,
            int? newSeverityId,
            string oldAssigneeId,
            string newAssigneeId,
            string oldReporterId,
            string newReporterId,
            int? oldWorklogId,
            int? newWorklogId,
            string oldDescription,
            string newDescription,
            string oldTitle,
            string newTitle,
            int? oldOriginEstimateTime,
            int? newOriginEstimateTime,
            int? oldRemainEstimateTime,
            int? newRemainEstimateTime,
            DateTimeOffset? oldDueDate,
            DateTimeOffset? newDueDate,
            string oldEnvironment,
            string newEnvironment,
            string oldToIssueId,
            string newToIssueId,
            int tagId)
        {
            Id = id;
            LogDate = DateTimeOffset.Now;
            Description = des;
            IssueId = issueId;
            ModifierId = modifierId;
            OldStatusTagId = preStatusTagId;
            OldStatusName = preStatusName;
            NewStatusTagId = modStatusTagId;
            NewStatusName = modStatusName;
            OldPriorityId = prePriorityId;
            NewPriorityId = modPriorityId;
            OldSeverityId = oldSeverityId;
            NewSeverityId = newSeverityId;
            OldAssigneeId = oldAssigneeId;
            NewAssigneeId = newAssigneeId;
            OldReporterId = oldReporterId;
            NewReporterId = newReporterId;
            OldWorklogId = oldWorklogId;
            NewWorklogId = newWorklogId;
            OldDescription = oldDescription;
            NewDescription = newDescription;
            OldTitle = oldTitle;
            NewTitle = newTitle;
            OldOriginEstimateTime = oldOriginEstimateTime;
            NewOriginEstimateTime = newOriginEstimateTime;
            OldRemainEstimateTime = oldRemainEstimateTime;
            NewRemainEstimateTime = newRemainEstimateTime;
            OldDueDate = oldDueDate;
            NewDueDate = newDueDate;
            OldEnvironment = oldEnvironment;
            NewEnvironment = newEnvironment;
            OldToIssueId = oldToIssueId;
            NewToIssueId = newToIssueId;
            TagId = tagId;
        }

        public void UpdateModifier(Account acc)
        {
            Modifier = acc;
        }
    }
}