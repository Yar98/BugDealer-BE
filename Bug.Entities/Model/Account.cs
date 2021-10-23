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
        public DateTime CreatedDate { get; private set; }
        public string ImageUri { get; private set; }
        public ICollection<Role> Roles { get; private set; }
        public ICollection<Project> CreatedProjects { get; private set; }

        private readonly List<Project> _projects = new List<Project>();
        public ICollection<Project> Projects => _projects.AsReadOnly();
        private Account() { }
        public Account(string id,
            string userName,
            string password,
            string firstName,
            string lastName,
            string email,
            DateTime createdDate,
            string imageUri)
        {
            Id = id;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedDate = createdDate;
            ImageUri = imageUri;
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
        public void AddRole(string roleId, 
            string name, 
            string memberId, 
            string description ="")
        {
            if (!Roles.Any(i => i.Id.Equals(roleId)))
            {
                Roles.Add(new Role(roleId, name, description, memberId));
            }
        }
        public void AddProject(string id,
            string name,
            DateTime startDate,
            DateTime endDate,
            string description,
            string creatorId,
            string workflowId)
        {
            if (!Projects.Any(i => i.Id.Equals(id)))
            {
                _projects.Add(new Project(id, name, startDate, endDate,description,creatorId,workflowId));
                //_projects.Add(p);
                return;
            }
        }
    }
}
