using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Issuelog : IEntityBase
    {
        public DateTime LogDate { get; private set; }
        public string IssueId { get; private set; }
        public Issue Issue { get; private set; }
        public string ModifierId { get; private set; }
        public Account Modifier { get; private set; }
        public string? PreStatusId { get; private set; }
        public Status PreStatus { get; private set; }
        public string? ModStatusId { get; private set; }
        public Status ModStatus { get; private set; }
        private Issuelog() { }
        public Issuelog
            (DateTime timeLog,
            string issueId,
            string modifierId,
            string preStatusId,
            string modStatusId)
        {
            LogDate = timeLog;
            IssueId = issueId;
            ModifierId = modifierId;
            PreStatusId = preStatusId;
            ModifierId = modStatusId;
        }
    }
}
