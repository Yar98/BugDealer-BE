﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Field : IEntityBase
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<Account> Accounts { get; private set; }

        private Field() { }
        
        public Field(int id, string name, string des)
        {
            Id = id;
            Name = name;
            Description = des;
        }
    }
}
