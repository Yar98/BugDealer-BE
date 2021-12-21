using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Severity : IEntityBase
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Icon { get; private set; }

        private Severity() { }
        public Severity
            (int id,
            string name,
            string description,
            string icon)
        {
            Id = id;
            Name = name;
            Description = description;
            Icon = icon;
        }
    }
}
