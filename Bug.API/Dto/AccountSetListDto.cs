using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class AccountSetListDto
    {
        public string AccountId { get; set; }
        public string ProjectId { get; set; }
        public List<RoleNormalDto> Roles { get; set; }
        public List<FieldNormalDto> Fields { get; set; }
    }
}
