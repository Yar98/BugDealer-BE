using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Builder
{
    public interface IIssuelogBuilder
    {
        IIssuelogBuilder AddId();
        IIssuelogBuilder AddDescription(string des);
        IIssuelogBuilder AddIssueId(string issueId);
        IIssuelogBuilder AddModifierId(string accountId);
        IIssuelogBuilder AddOldStatusTagId(int? id);
        IIssuelogBuilder AddOldStatusName(string statusId);
        IIssuelogBuilder AddNewStatusTagId(int? id);
        IIssuelogBuilder AddNewStatusName(string statusId);
        IIssuelogBuilder AddOldPriorityId(int? priorityId);
        IIssuelogBuilder AddNewPriorityId(int? priorityId);
        IIssuelogBuilder AddOldSeverityId(int? id);
        IIssuelogBuilder AddNewSeverityId(int? id);
        IIssuelogBuilder AddOldAssigneeId(string id);
        IIssuelogBuilder AddNewAssigneeId(string id);
        IIssuelogBuilder AddOldReporterId(string id);
        IIssuelogBuilder AddNewReporterId(string id);
        IIssuelogBuilder AddOldWorklogId(int? id);
        IIssuelogBuilder AddNewWorklogId(int? id);
        IIssuelogBuilder AddOldDescription(string des);
        IIssuelogBuilder AddNewDescription(string des);
        IIssuelogBuilder AddOldTitle(string title);
        IIssuelogBuilder AddNewTitle(string title);
        IIssuelogBuilder AddOldOriginEstimateTime(string time);
        IIssuelogBuilder AddNewOriginEstimateTime(string time);
        IIssuelogBuilder AddOldRemainEstimateTime(string time);
        IIssuelogBuilder AddNewRemainEstimateTime(string time);
        IIssuelogBuilder AddOldDueDate(DateTimeOffset? dt);
        IIssuelogBuilder AddNewDueDate(DateTimeOffset? dt);
        IIssuelogBuilder AddOldEnvironment(string dt);
        IIssuelogBuilder AddNewEnvironment(string dt);
        IIssuelogBuilder AddOldToIssueId(string id);
        IIssuelogBuilder AddNewToIssueId(string id);
        IIssuelogBuilder AddTagId(int tagId);

        Issuelog Build();
    }
}