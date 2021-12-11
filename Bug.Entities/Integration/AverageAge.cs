using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Integration
{
    public class AverageAge
    {
        public DateTimeOffset ItemDate { get; set; }
        public int TotalAge { get; set; }
        public int AvgAge { get; set; }
    }
}
