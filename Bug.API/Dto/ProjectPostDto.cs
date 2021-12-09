using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class ProjectPostDto
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(64)]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public DateTimeOffset? RecentDate { get; set; }
        public string AvatarUri { get; set; }
        public string Description { get; set; }
        public int? TemplateId { get; set; }
        public string DefaultAssigneeId { get; set; }
        [Required]
        public string CreatorId { get; set; }
    }
}
