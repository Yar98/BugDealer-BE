using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class AccountSetRolesDto
    {
        public string AccountId { get; set; }
        public string ProjectId { get; set; }
        public List<Role> Roles { get; set; }
    }
}
