using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class ErrorNormalDto
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }
}
