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
        IIssuelogBuilder AddLogDate(DateTimeOffset d);
        IIssuelogBuilder AddIssueId(string issueId);
        IIssuelogBuilder AddModifierId(string accountId);
        IIssuelogBuilder AddPreStatusId(string statusId);
        IIssuelogBuilder AddModStatusId(string statusId);
        IIssuelogBuilder AddPrePriorityId(int priorityId);
        IIssuelogBuilder AddModPriorityId(int priorityId);
        IIssuelogBuilder AddTagId(int tagId);

        Issuelog Build();
    }
}
