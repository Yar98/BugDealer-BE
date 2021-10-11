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
        public int RemainTime { get; private set; }
        public DateTime LogDate { get; private set; }
        public string LoggerId { get; private set; }
        public Account Logger { get; private set; }
        private Worklog() { }
        public Worklog(int id,
            int spentTime,
            int remainTime,
            DateTime logDate,
            string loggerId)
        {
            Id = id;
            SpentTime = spentTime;
            RemainTime = remainTime;
            LogDate = logDate;
            LoggerId = loggerId;
        }
    }
}
