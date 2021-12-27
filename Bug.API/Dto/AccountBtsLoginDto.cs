using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class AccountBtsLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MaxLength(32)]
        [MinLength(8)]
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}
