using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Builder
{
    public class ProjectBuilder : IProjectBuilder
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public DateTimeOffset? StartDate { get; private set; }
        public DateTimeOffset? EndDate { get; private set; }
        public DateTimeOffset? RecentDate { get; private set; }
        public string Description { get; private set; }
        public string AvatarUri { get; private set; }
        public string DefaultAssigneeId { get; private set; }
        public string CreatorId { get; private set; }
        public int State { get; private set; }
        public int TemplateId { get; private set; }
        public string DefaultStatusId { get; private set; }
        public int DefaultRoleId { get; private set; }

        public IProjectBuilder AddDefaultRoleId(int id)
        {
            DefaultRoleId = id;
            return this;
        }

        public IProjectBuilder AddDefaultStatusId(string id)
        {
            DefaultStatusId = id;
            return this;
        }

        public IProjectBuilder AddState()
        {
            State = 1;
            return this;
        }
        public IProjectBuilder AddTemplateId(int id)
        {
            TemplateId = id;
            return this;
        }
        public IProjectBuilder AddCreatorId(string id)
        {
            CreatorId = id;
            return this;
        }

        public IProjectBuilder AddCode(string code)
        {
            Code = code;
            return this;
        }

        public IProjectBuilder AddDescription(string des)
        {
            Description = des;
            return this;
        }

        public IProjectBuilder AddEndDate(DateTimeOffset? date)
        {
            EndDate = date;
            return this;
        }

        public IProjectBuilder AddId(string id)
        {
            Id = id;
            return this;
        }

        public IProjectBuilder AddName(string name)
        {
            Name = name;
            return this;
        }

        public IProjectBuilder AddStartDate(DateTimeOffset? date)
        {
            StartDate = date;
            return this;
        }
        
        public IProjectBuilder AddRecentDate(DateTimeOffset? date)
        {
            RecentDate = date;
            return this;
        }

        public IProjectBuilder AddDefaultAssigneeId(string id)
        {
            DefaultAssigneeId = id;
            return this;
        }

        public IProjectBuilder AddAvatarUri(string uri)
        {
            AvatarUri = uri;
            return this;
        }

        public Project Build()
        {
            return new Project(Id, Name, Code, StartDate, EndDate, RecentDate, Description, AvatarUri, DefaultAssigneeId, DefaultStatusId, DefaultRoleId, CreatorId, TemplateId, State);
        }
    }
}
