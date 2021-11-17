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
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Email { get; private set; }
        public string Language { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; }
        public string ImageUri { get; private set; }
        public string? TimezoneId { get; private set; }
        public Timezone Timezone { get; private set; }
        public ICollection<Project> CreatedProjects { get; private set; }
        public ICollection<Role> CreatedRoles { get; private set; }
        public ICollection<Status> CreatedStatuses { get; private set; }
        public ICollection<Project> DefaultAssigneeProjects { get; private set; }
        public ICollection<Issue> WatchIssues { get; private set; }
        public ICollection<Issue> VoteIssues { get; private set; }
        public ICollection<Issue> ReportIssues { get; private set; }
        public ICollection<Issue> AssignIssues { get; private set; }
        public ICollection<Role> Roles { get; private set; }
        public ICollection<Customtype> Customtype { get; private set; }

        private readonly List<Project> _projects = new List<Project>();
        public ICollection<Project> Projects => _projects.AsReadOnly();

        private Account() { }
        public Account(string id,
            string userName,
            string password,
            string firstName,
            string lastName,
            string email,
            DateTimeOffset createdDate,
            string language,
            string imageUri,
            string timeZone)
        {
            Id = id;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedDate = createdDate;
            ImageUri = imageUri;
            TimezoneId = timeZone;
            Language = language;
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
        public void UpdateLanguage(string lan)
        {
            Language = lan;
        }


        public void AddNewRole
            (string name, 
            string memberId, 
            string description)
        {           
            Roles.Add(new Role(0,name, description, memberId));
            return;
        }

        public void AddProject
            (string id,
            string name,
            string code,
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            DateTimeOffset recentDate,
            string description,
            string uri,
            string defaultAssigneeId,
            string creatorId,
            int templateId,
            int tagId)
        {
            if (!Projects.Any(i => i.Id.Equals(id)))
            {
                _projects.Add(new Project(id, name, code, startDate, endDate, recentDate, description, uri, defaultAssigneeId, creatorId,templateId,tagId));
                return;
            }
        }
    }
}
