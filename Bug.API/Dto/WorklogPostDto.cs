using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class WorklogPostDto
    {
        public string SpentTime { get; set; }
        public string RemainTime { get; set; }
        public DateTimeOffset LogDate { get; set; }
        public string LoggerId { get; set; }
        public string Description { get; set; }
    }
}
