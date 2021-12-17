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
        public string? PreStatusId { get; private set; }
        public Status PreStatus { get; private set; }
        // new status
        public string? ModStatusId { get; private set; }
        public Status ModStatus { get; private set; }
        // old priority
        public int? PrePriorityId { get; private set; }
        public Priority PrePriority { get; private set; }
        // new priority
        public int? ModPriorityId { get; private set; }
        public Priority ModPriority { get; private set; }
        // action
        public int? TagId { get; private set; }
        public Tag Tag { get; private set; }

        private Issuelog() { }
        public Issuelog
            (int id,
            DateTimeOffset timeLog,
            string des,
            string issueId,
            string modifierId,
            string preStatusId,
            string modStatusId,
            int prePriorityId,
            int modPriorityId,
            int tagId)
        {
            Id = id;
            LogDate = timeLog;
            Description = des;
            IssueId = issueId;
            ModifierId = modifierId;
            PreStatusId = preStatusId;
            ModStatusId = modStatusId;
            PrePriorityId = prePriorityId;
            ModPriorityId = modPriorityId;
            TagId = tagId;
        }

        public void UpdateModifier(Account acc)
        {
            Modifier = acc;
        }
    }
}