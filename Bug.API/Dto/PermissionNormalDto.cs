using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class PermissionNormalDto
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public bool Active { get; set; }
    }
}
