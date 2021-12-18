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
        public bool VerifyEmail { get; private set; } = false;
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
        public ICollection<Project> RelateProjects { get; private set; }

        private List<Field> _fields = new();
        public ICollection<Field> Fields => _fields.AsReadOnly();

        public ICollection<AccountProjectRole> AccountProjectRoles { get; set; } = new List<AccountProjectRole>();

        private Account() { }
        public Account
            (string id,
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
            if (username == null)
                return;
            if (username == "")
                username = null;
            UserName = username;
        }
        public void UpdatePassword(string pass)
        {
            if (string.IsNullOrEmpty(pass))
                return;
            Password = pass;
        }
        public void UpdateFirstName(string firstname)
        {
            if (firstname == null)
                return;
            if (firstname == "")
                firstname = null;
            FirstName = firstname;
        }
        public void UpdateLastName(string lastname)
        {
            if (lastname == null)
                return;
            if (lastname == "")
                lastname = null;
            LastName = lastname;
        }
        public void UpdateEmail(string email)
        {
            if (email == null)
                return;
            if (email == "")
                email = null;
            Email = email;
        }
        public void UpdateImageUri(string imageuri)
        {
            if (imageuri == null)
                return;
            if (imageuri == "")
                imageuri = null;
            ImageUri = imageuri;
        }
        public void UpdateLanguage(string lan)
        {
            if (lan == null)
                return;
            if (lan == "")
                lan = null;
            Language = lan;
        }
        public void UpdateVerifyEmail(bool i)
        {
            VerifyEmail = i;
        }

        public void UpdateTimezoneId(string timezoneId)
        {
            if (timezoneId == null)
                return;
            if (timezoneId == "")
                timezoneId = null;
            TimezoneId = timezoneId;
        }

        public void UpdateFields(IReadOnlyList<Field> l)
        {
            if (l == null)
                return;
            _fields.Clear();
            _fields.AddRange(l);
        }
    }
}
