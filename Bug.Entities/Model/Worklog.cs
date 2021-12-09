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
        public string SpentTime { get; private set; }
        public string RemainTime { get; private set; }
        public DateTimeOffset LogDate { get; private set; }
        public string LoggerId { get; private set; }
        public Account Logger { get; private set; }
        private Worklog() { }
        public Worklog
            (int id,
            string spentTime,
            string remainTime,
            DateTimeOffset logDate,
            string loggerId)
        {
            Id = id;
            SpentTime = spentTime;
            RemainTime = remainTime;
            LogDate = logDate;
            LoggerId = loggerId;
        }

        public void UpdateSpentTime(string s)
        {
            SpentTime = s;
        }

        public void UpdateRemainTime(string s)
        {
            RemainTime = s;
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
