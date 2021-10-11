using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Role : IEntityBase
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string MemberId { get; private set; }
        public ICollection<Account> Accounts { get; private set; }
        private Role() { }
        public Role(string id,
            string name,
            string description,
            string memberId)
        {
            Id = id;
            Name = name;
            Description = description;
            MemberId = memberId;
        }
    }
}
