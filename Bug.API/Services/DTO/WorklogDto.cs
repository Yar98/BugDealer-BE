using System;

namespace Bug.Entities.Dtos
{
    public class WorklogDto
    {
        public int Id { get; private set; }
        public int SpentTime { get; private set; }
        public int RemainTime { get; private set; }
        public DateTime LogDate { get; private set; }
        public string LoggerId { get; private set; }
    }
}