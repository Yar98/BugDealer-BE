using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class TagNormalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int CategoryId { get; set; }
        public List<StatusNormalDto> Statuses { get; set; }
        public List<IssueNormalDto> Issues { get; set; }
        public List<ProjectNormalDto> Projects { get; set; }
    }
}
