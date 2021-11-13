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
        public string CreatorId { get; set; }
        public string ProjectId { get; set; } // current project which user choose
        public List<Account> Accounts { get; set; }
        public List<Permission> Permissions { get; set; }
        public List<Project> Projects { get; set; }
    }
}
