using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Builder
{
    public interface IIssueBuilder
    {
        IIssueBuilder AddId(string id);
        IIssueBuilder AddTitle(string title);
        IIssueBuilder AddDescription(string des);
        IIssueBuilder AddNumberCode();
        IIssueBuilder AddLogDate(DateTimeOffset? tl);
        IIssueBuilder AddCreatedDate(DateTimeOffset? cd);
        IIssueBuilder AddDueDate(DateTimeOffset? dd);
        IIssueBuilder AddWorklogDate(DateTimeOffset? dd);
        IIssueBuilder AddOriginEstimateTime(string oet);
        IIssueBuilder AddRemainEstimateTime(string ret);
        IIssueBuilder AddEnvironment(string e);
        IIssueBuilder AddStatusId(string s);
        IIssueBuilder AddPriorityId(int p);
        IIssueBuilder AddProjectId(string s);
        IIssueBuilder AddReporterId(string s);
        IIssueBuilder AddAssigneeId(string s);
        IIssueBuilder AddWorklogId(int i);
        Issue Build();
    }
}
