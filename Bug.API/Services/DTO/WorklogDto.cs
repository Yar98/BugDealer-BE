using System;

namespace Bug.API.Services.DTO
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