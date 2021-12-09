using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class WorklogPutDto
    {
        public int Id { get; set; }
        public string SpentTime { get; set; }
        public string RemainTime { get; set; }
        public DateTimeOffset LogDate { get; set; }
        public string LoggerId { get; set; }
    }
}
