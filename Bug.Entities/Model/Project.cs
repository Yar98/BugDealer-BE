using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.ModelException;

namespace Bug.Entities.Model
{
    public class Project : IEntityBase
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public int Temp { get; set; } = 1;
        public DateTimeOffset? StartDate { get; private set; }
        public DateTimeOffset? EndDate { get; private set; }
        public DateTimeOffset? RecentDate { get; private set; } // ko can
        public string Description { get; private set; }
        public string AvatarUri { get; private set; }
        public int State { get; private set; }
        public string? DefaultAssigneeId { get; private set; }
        public Account DefaultAssignee { get; private set; }
        public int? DefaultRoleId { get; private set; }
        public Role DefaultRole { get; private set; }
        public string? DefaultStatusId { get; private set; }
        public Status DefaultStatus { get; private set; }
        public string CreatorId { get; private set; }
        public Account Creator { get; private set; }
        public int? TemplateId { get; private set; }
        public Template Template { get; private set; }

        
        public ICollection<Account> Relator { get; private set; }
        
        private readonly List<Issue> _issues = new List<Issue>();
        public ICollection<Issue> Issues => _issues.AsReadOnly();

        private List<Role> _roles = new List<Role>();
        public ICollection<Role> Roles => _roles.AsReadOnly();

        private List<Status> _statuses = new List<Status>();
        public ICollection<Status> Statuses => _statuses.AsReadOnly();

        public ICollection<AccountProjectRole> AccountProjectRoles { get; set; } = new List<AccountProjectRole>();

        public int? TotalIssues 
        { 
            get
            {
                if(Issues != null)
                    return Issues.Count();
                return null;
            }
        }
        public int? TotalOpenIssues 
        { 
            get
            {
                if (Issues != null)
                    return Issues
                        .Where(i => i.Status?.TagId == 1 || i.Status?.TagId == 2)
                        .Count();
                return null;
            }
        }

        public void UpdateCreatorId(string testAccountId)
        {
            CreatorId = testAccountId;
        }

        public int? TotalInProgressIssues
        {
            get
            {
                if (Issues != null)
                    return Issues
                        .Where(i => i.Status?.TagId == 2)
                        .Count();
                return null;
            }
        }
        public int? TotalDoneIssues 
        {
            get
            {
                if (Issues != null)
                    return Issues
                        .Where(i => i.Status?.TagId == 3)
                        .Count();
                return null;
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
            string defaultStatusId,
            int? defaultRoleId,
            string creatorId,
            int? templateId,
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
            State = tagId;
            DefaultStatusId = defaultStatusId;
            DefaultRoleId = defaultRoleId;
        }

        public void UpdateName(string name)
        {
            if (name == null)
                return;
            if (name == "")
                Name = null;
            Name = name;            
        }
        public void UpdateAvatarUri(string uri)
        {
            if (uri == null)
                return;
            if (uri == "")
                AvatarUri = null;
            AvatarUri = uri;
        }
        public void UpdateCode(string code)
        {
            if (code == null)
                return;
            if (code == "")
                Code = null;
            Code = code;
        }
        public void UpdateDescription(string des)
        {
            if (des == null)
                return;
            if (des == "")
                Description = null;
            Description = des;
        }
        public void UpdateStartDate(string d)
        {
            if (d == "")
                StartDate = null;
            else if(d != null)
                StartDate = DateTimeOffset.Parse(d);
        }
        public void UpdateEndDate(string d)
        {
            if (d == "")
                EndDate = null;
            else if(d != null)
            {
                var temp = DateTimeOffset.Parse(d);
                if (Issues.Count != 0 && Issues.Any(i => i.DueDate > temp))
                    throw new DueDateOfIssueMustWithinDueDateOfProject();
                EndDate = temp;
            }
                
        }
        public void UpdateDefaultAssigneeId(string id)
        {
            if (id == null)
                return;
            if (id == "")
                DefaultAssigneeId = null;
            DefaultAssigneeId = id;
        }
        public void UpdateTemplateId(int? id)
        {
            TemplateId = id?? TemplateId;
        }
        public void UpdateDefaultStatusId(string id)
        {
            if (id == null)
                return;
            if (id == "")
                DefaultStatusId = null;
            DefaultStatusId = id;
        }
        public void UpdateDefaultRoleId(int? id)
        {
            DefaultRoleId = id?? DefaultRoleId;
        }
        public void UpdateState(int? id)
        {
            State = id??State;
        }

        public void UpdateRoles(List<Role> r)
        {
            if(r != null)
                _roles = r;
        }

        public void UpdateTemplate(Template t)
        {
            Template = t;
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
            if (r == null)
                return;
            _statuses = r;
        }
        public void AddDefaultStatuses(IReadOnlyList<Status> statuses)
        {
            if (!_statuses.Any())
            {
                _statuses.AddRange(statuses);
            }
        }

        public void UpdateIssues(List<Issue> li)
        {
            _issues.Clear();
            _issues.AddRange(li);
        }

    }
}
