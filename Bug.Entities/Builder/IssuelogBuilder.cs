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
        public string PreStatusId { get; private set; }
        public string ModStatusId { get; private set; }
        public int PrePriorityId { get; private set; }
        public int ModPriorityId { get; private set; }
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

        public IIssuelogBuilder AddModPriorityId(int priorityId)
        {
            ModPriorityId = priorityId;
            return this;
        }

        public IIssuelogBuilder AddModStatusId(string statusId)
        {
            ModStatusId = statusId;
            return this;
        }

        public IIssuelogBuilder AddPrePriorityId(int priorityId)
        {
            PrePriorityId = priorityId;
            return this;
        }

        public IIssuelogBuilder AddPreStatusId(string statusId)
        {
            PreStatusId = statusId;
            return this;
        }

        public IIssuelogBuilder AddTagId(int tagId)
        {
            TagId = tagId;
            return this;
        }

        public Issuelog Build()
        {
            return new Issuelog(Id, LogDate, Description, IssueId, ModifierId, PreStatusId, ModStatusId, PrePriorityId, ModPriorityId, TagId);
        }
    }
}