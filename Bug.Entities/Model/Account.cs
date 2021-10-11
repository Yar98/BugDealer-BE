using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Account : IEntityBase
    {
        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string CreatedDate { get; private set; }
        public string ImageUri { get; private set; }
        public int? ProviderId { get; private set; }
        public Provider Provider { get; private set; }
        public Project CreatedProject { get; private set; }
        public ICollection<Role> Roles { get; private set; }
        public ICollection<Project> Projects { get; private set; }
        private Account() { }
        public Account(string id,
            string username,
            string password,
            string firstname,
            string lastname,
            string email,
            string createddate,
            string imageuri,
            int providerid)
        {
            Id = id;
            UserName = username;
            Password = password;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            CreatedDate = createddate;
            ImageUri = imageuri;
            ProviderId = providerid;
        }
        public void UpdateUserName(string username)
        {
            UserName = username;
        }
        public void UpdateFirstName(string firstname)
        {
            FirstName = firstname;
        }
        public void UpdateLastName(string lastname)
        {
            LastName = lastname;
        }
        public void UpdateEmail(string email)
        {
            Email = email;
        }
        public void UpdateImageUri(string imageuri)
        {
            ImageUri = imageuri;
        }
        public void AddRole(string roleId, string name, string memberId, string description ="")
        {
            if (!Roles.Any(i => i.Id.Equals(roleId)))
            {
                Roles.Add(new Role(roleId, name, description, memberId));
            }
        }
    }
}
