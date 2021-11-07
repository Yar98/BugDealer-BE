using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Category : IEntityBase
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        private Category() { }
        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
