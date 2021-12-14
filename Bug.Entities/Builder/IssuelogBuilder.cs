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
        public DateTimeOffset LogDate { get; private set; }
        public string Description { get; private set; }
        public string IssueId { get; private set; }
        public string ModifierId { get; private set; }
        public string PreStatus { get; private set; }
        public string ModStatus { get; private set; }
        public string PrePriority { get; private set; }
        public string ModPriority { get; private set; }
        public int TagId { get; set; }

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

        public IIssuelogBuilder AddLogDate()
        {
            LogDate = DateTimeOffset.Now;
            return this;
        }

        public IIssuelogBuilder AddModifierId(string accountId)
        {
            ModifierId = accountId;
            return this;
        }

        public IIssuelogBuilder AddModPriority(string priority)
        {
            ModPriority = priority;
            return this;
        }

        public IIssuelogBuilder AddModStatus(string status)
        {
            ModStatus = status;
            return this;
        }

        public IIssuelogBuilder AddPrePriority(string priority)
        {
            PrePriority = priority;
            return this;
        }

        public IIssuelogBuilder AddPreStatus(string status)
        {
            PreStatus = status;
            return this;
        }

        public IIssuelogBuilder AddTagId(int tagId)
        {
            TagId = tagId;
            return this;
        }

        public Issuelog Build()
        {
            return new Issuelog(Id, LogDate, Description, IssueId, ModifierId, PreStatus, ModStatus, PrePriority, ModPriority, TagId);
        }
    }
}
