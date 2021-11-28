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
        public DateTimeOffset? StartDate { get; private set; }
        public DateTimeOffset? EndDate { get; private set; }
        public DateTimeOffset? RecentDate { get; private set; }
        public string Description { get; private set; }
        public string AvatarUri { get; private set; }
        public int Status { get; private set; }
        public string DefaultAssigneeId { get; private set; }
        public Account DefaultAssignee { get; private set; }
        public string CreatorId { get; private set; }
        public Account Creator { get; private set; }
        public int TemplateId { get; private set; }
        public Template Template { get; private set; }
        
        private readonly List<Issue> _issues = new List<Issue>();
        public ICollection<Issue> Issues => _issues.AsReadOnly();

        private readonly List<Account> _accounts = new List<Account>();
        public ICollection<Account> Accounts => _accounts.AsReadOnly();

        private List<Role> _roles = new List<Role>();
        public ICollection<Role> Roles => _roles.AsReadOnly();

        private List<Status> _statuses = new List<Status>();
        public ICollection<Status> Statuses => _statuses.AsReadOnly();

        public int TotalIssues 
        { 
            get
            {
                return Issues.Count();
            }
        }
        public int TotalOpenIssues 
        { 
            get
            {
                return Issues
                    .Where(i => i.Status.TagId == 1)
                    .Count();
            }
        }
        public int TotalInProgressIssues
        {
            get
            {
                return Issues
                    .Where(i => i.Status.TagId == 2)
                    .Count();
            }
        }
        public int TotalDoneIssues 
        {
            get
            {
                return Issues
                    .Where(i => i.Status.TagId == 3)
                    .Count();
            }
        }

        private Project() { }
        public Project(string id,
            string name,
            string code,
            DateTimeOffset? startDate,
            DateTimeOffset? endDate,
            DateTimeOffset? recentDate,
            string description,
            string uri,
            string defaultAssigneeId,
            string creatorId,
            int templateId,
            int tagId)
        {
            Id = id;
            Name = name;
            Code = code;
            StartDate = startDate;
            EndDate = endDate;
            RecentDate = recentDate;
            Description = description;
            AvatarUri = uri;
            DefaultAssigneeId = defaultAssigneeId;
            CreatorId = creatorId;
            TemplateId = templateId;
            Status = tagId;
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
        public void UpdateDescription(string des)
        {
            Description = des;
        }
        public void UpdateStartDate(DateTimeOffset? d)
        {
            StartDate = d;
        }
        public void UpdateEndDate(DateTimeOffset? d)
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
        public void UpdateTemplateId(int id)
        {
            TemplateId = id;
        }
        public void UpdateStatus(int id)
        {
            Status = id;
        }

        public void AddExistAccount(Account a)
        {
            if (!Accounts.Any(i => i.Id.Equals(a.Id)))
            {
                _accounts.Add(a);
                return;
            }
        }
        public void UpdateRoles(List<Role> r)
        {
            _roles = r;
        }
        public void AddExistRole(Role r)
        {
            if(!Roles.Any(i=>i.Id == r.Id))
            {
                _roles.Add(r);
                return;
            }
        }
        public void AddDefaultRoles(IReadOnlyList<Role> roles)
        {
            if (!Roles.Any())
            {
                _roles.AddRange(roles);
            }
        }
        public void UpdateStatuses(List<Status> r)
        {
            _statuses = r;
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
