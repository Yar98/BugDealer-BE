using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Integration
{
    public class RelatedIssues
    {
        public Tag Tag { get; set; }
        public List<Issue> Issues { get; set; }
    }
}
