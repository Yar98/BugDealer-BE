using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class AccountPutWithCheckDto
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string ImageUri { get; set; }
        public string TimezoneId { get; set; }
        public bool VerifyEmail { get; set; }
    }
}
