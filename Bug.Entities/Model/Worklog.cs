using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Worklog : IEntityBase
    {
        public int Id { get; private set; }
        public int SpentTime { get; private set; }
        public DateTimeOffset LogDate { get; private set; }
        public string IssueId { get; private set; }
        public Issue Issue { get; private set; }
        public string LoggerId { get; private set; }
        public Account Logger { get; private set; }
        public string Description { get; set; }
        private Worklog() { }
        public Worklog
            (int id,
            int spentTime,
            DateTimeOffset logDate,
            string issueId,
            string loggerId)
        {
            Id = id;
            LogDate = logDate;
            IssueId = issueId;
            SpentTime = spentTime;
            LoggerId = loggerId;
        }

        public int GetSpentTime()
        {
            return SpentTime;
        }

        public void UpdateSpentTime(int s)
        {
            SpentTime = s;
        }

        public void UpdateLogDate(DateTimeOffset d)
        {
            LogDate = d;
        }

        public void UpdateLoggerId(string id)
        {
            LoggerId = id;
        }
    }
}
