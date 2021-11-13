using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Dto
{
    public class FieldNormalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public ICollection<Customtype> Customtypes { get; set; }
    }
}
