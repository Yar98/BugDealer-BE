using System;
using System.Collections.Generic;

namespace Bug.API.Dto
{
    public class AccountDetailDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImageUri { get; set; }
        public string TimezoneId { get; set; }

    }
}