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

        private List<Permission> _permissions = new();
        public ICollection<Permission> Permissions => _permissions.AsReadOnly();

        public ICollection<Project> DefaultInProjects { get; private set; }
        public ICollection<Project> Projects { get; private set; }
        public ICollection<AccountProjectRole> AccountProjectRoles { get; set; } = new List<AccountProjectRole>();

        private Role() { }
        public Role
            (int id,
            string name,
            string description,
            string creatorId)
        {
            Id = id;
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

        public void UpdatePermission(List<Permission> ps)
        {
            if (ps != null && ps.Any())
            {
                _permissions = ps;
            }
        }
    }
}
