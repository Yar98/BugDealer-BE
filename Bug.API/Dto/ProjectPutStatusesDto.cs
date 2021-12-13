using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class ProjectPutStatusesDto
    {
        public string Id { get; set; }
        public List<StatusNormalDto> Statuses { get; set; }
        public List<LineDto> OldStatuses { get; set; }
        public string DefaultStatusId { get; set; }
    }
}
