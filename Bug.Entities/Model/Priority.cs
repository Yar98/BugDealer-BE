using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Priority : IEntityBase
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        private Priority() { }
        public Priority(int id,
            string name,
            string description)
        {
            Id = id;
            Name = Name;
            Description = description;
        }
    }
}
