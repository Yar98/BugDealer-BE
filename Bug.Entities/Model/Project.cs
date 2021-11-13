using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Project : IEntityBase, IIntegrationBase
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string ProjectType { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime RecentDate { get; private set; }
        public string Description { get; private set; }
        public string AvatarUri { get; private set; }
        public string DefaultAssigneeId { get; private set; }
        public Account DefaultAssignee { get; private set; }
        public string CreatorId { get; private set; }
        public Account Creator { get; private set; }

        private readonly List<Tag> _tags = new List<Tag>();
        public ICollection<Tag> Tags => _tags.AsReadOnly();

        private readonly List<Issue> _issues = new List<Issue>();
        public ICollection<Issue> Issues { get; private set; }

        private readonly List<Account> _accounts = new List<Account>();
        public ICollection<Account> Accounts => _accounts.AsReadOnly();

        private readonly List<Role> _roles = new List<Role>();
        public ICollection<Role> Roles => _roles.AsReadOnly();

        private readonly List<Status> _statuses = new List<Status>();
        public ICollection<Status> Statuses => _statuses.AsReadOnly();

        private Project() { }
        public Project(string id,
            string name,
            string code,
            string projectType,
            DateTime startDate,
            DateTime endDate,
            DateTime recentDate,
            string description,
            string uri,
            string defaultAssigneeId,
            string creatorId)
        {
            Id = id;
            Name = name;
            Code = code;
            ProjectType = projectType;
            StartDate = startDate;
            EndDate = endDate;
            RecentDate = recentDate;
            Description = description;
            AvatarUri = uri;
            DefaultAssigneeId = defaultAssigneeId;
            CreatorId = creatorId;
            //WorkflowId = workflowId;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }
        public void UpdateAvatarUri(string uri)
        {
            AvatarUri = uri;
        }
        public void UpdateCode(string code)
        {
            Code = code;
        }
        public void UpdateProjectType(string s)
        {
            ProjectType = s;
        }
        public void UpdateDescription(string des)
        {
            Description = des;
        }
        public void UpdateStartDate(DateTime d)
        {
            StartDate = d;
        }
        public void UpdateEndDate(DateTime d)
        {
            EndDate = d;
        }
        public void UpdateCreatorId(string id)
        {
            CreatorId = id;
        }
        public void UpdateDefaultAssigneeId(string id)
        {
            DefaultAssigneeId = id;
        }

        public void AddAccount(Account a)
        {
            if (!Accounts.Any(i => i.Id.Equals(a.Id)))
            {
                _accounts.Add(a);
                return;
            }
        }

        public void AddTag(Tag t)
        {
            if (!Tags.Any(i => i.Id.Equals(t.Id)))
            {
                _tags.Add(t);
                return;
            }
        }

        public void AddExistRole(Role r)
        {
            if(!Roles.Any(i => i.Id == r.Id))
            {
                _roles.Add(r);
                return;
            }
        }

        public void AddNewRole
            (string name,
            string description,
            string creatorId)
        {
            _roles.Add(new Role(name, description, creatorId));
        }

        public void AddDefaultRoles(IReadOnlyList<Role> roles)
        {
            if (!Roles.Any())
            {
                _roles.AddRange(roles);
            }
        }

        public void AddDefaultStatuses(IReadOnlyList<Status> statuses)
        {
            if (!_statuses.Any())
            {
                _statuses.AddRange(statuses);
            }
        }

    }
}
