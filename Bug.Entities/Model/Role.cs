using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Role : IEntityBase
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string CreatorId { get; private set; }
        public Account Creator { get; private set; }
        public ICollection<Account> Accounts { get; private set; }
        public ICollection<Permission> Permissions { get; private set; }
        public ICollection<Project> Projects { get; private set; }
        
        private Role() { }
        public Role
            (string name,
            string description,
            string creatorId)
        {
            Name = name;
            Description = description;
            CreatorId = creatorId;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateDescription(string des)
        {
            Description = des;
        }
        
        public void UpdateCreatorId(string id)
        {
            CreatorId = id;
        }
    }
}
