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
        IIssuelogBuilder AddLogDate();
        IIssuelogBuilder AddDescription(string des);
        IIssuelogBuilder AddIssueId(string issueId);
        IIssuelogBuilder AddModifierId(string accountId);
        IIssuelogBuilder AddPreStatus(string status);
        IIssuelogBuilder AddModStatus(string status);
        IIssuelogBuilder AddPrePriority(string priority);
        IIssuelogBuilder AddModPriority(string priority);
        IIssuelogBuilder AddTagId(int tagId);

        Issuelog Build();
    }
}
