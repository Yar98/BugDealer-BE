using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Services.DTO
{
    public class AccountGoogleDto
    {
        public string GoogleId { get; set; }
        public string UserName { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
    }
}
