using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class RoleNormalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Default { get; set; }
        public string CreatorId { get; set; }
        public List<AccountNormalDto> Accounts { get; set; }
        public List<PermissionNormalDto> Permissions { get; set; }
        public List<ProjectPostDto> Projects { get; set; }
    }
}
