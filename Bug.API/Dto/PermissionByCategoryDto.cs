using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class PermissionByCategoryDto
    {
        public List<Permission> ProjectPermissions { get; set; }
        public List<Permission> IssueTrackingPermissions { get; set; }
    }
}
