using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Template : IEntityBase
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private Template() { }
        public Template(int id, string name, string des)
        {
            Id = id;
            Name = name;
            Description = des;
        }
    }
}
