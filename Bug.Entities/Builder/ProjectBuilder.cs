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
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public DateTimeOffset RecentDate { get; private set; }
        public string Description { get; private set; }
        public string ProjectType { get; private set; }
        public string AvatarUri { get; private set; }
        public string DefaultAssigneeId { get; private set; }
        public string CreatorId { get; private set; }
        //public string WorkflowId { get; private set; }

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

        public IProjectBuilder AddEndDate(DateTimeOffset date)
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

        public IProjectBuilder AddStartDate(DateTimeOffset date)
        {
            StartDate = date;
            return this;
        }
        
        public IProjectBuilder AddRecentDate(DateTimeOffset date)
        {
            RecentDate = date;
            return this;
        }

        public IProjectBuilder AddProjectType(string t)
        {
            ProjectType = t;
            return this;
        }

        /*
        public IProjectBuilder AddWorkflowId(string id)
        {
            WorkflowId = id;
            return this;
        }
        */

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
            return new Project(Id, Name, Code, ProjectType, StartDate, EndDate, RecentDate, Description, AvatarUri, DefaultAssigneeId, CreatorId);
        }
    }
}
