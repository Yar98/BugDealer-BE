using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Label:IEntityBase
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        private Label() { }
        public Label(int id,
            string name,
            string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
