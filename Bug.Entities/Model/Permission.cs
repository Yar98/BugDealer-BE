using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Permission : IEntityBase
    {
        public int Id { get; private set; }
        public string Action { get; private set; }
        public ICollection<Role> Roles { get; private set; }
        private Permission() { }
        public Permission(int id, string action)
        {
            Id = id;
            Action = action;
        }
    }
}
