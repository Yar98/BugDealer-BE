using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class AccountBtsRegister
    {
        [Required]
        [MaxLength(64)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(32)]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public int TimeZoneId { get; set; }
        public string Language { get; set; }
    }
}
