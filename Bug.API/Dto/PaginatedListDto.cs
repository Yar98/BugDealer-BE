using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class PaginatedListDto<T>
    {
        public int Length { get; set; }
        public List<T> Items { get; set; }
    }
}
