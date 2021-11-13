using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Status : IEntityBase
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Progress { get; private set; }
        public string CreatorId { get; private set; }
        public Account Creator { get; private set; }

        private readonly List<Tag> _tags = new List<Tag>();
        public ICollection<Tag> Tags => _tags.AsReadOnly();

        private readonly List<Project> _projects = new List<Project>();
        public ICollection<Project> Projects => _projects.AsReadOnly();

        //public ICollection<Workflow> Workflows { get; private set; }
        private Status() { }
        public Status(string id,
            string name,
            string description,
            int progress,
            string creatorId)
        {
            Id = id;
            Name = name;
            Description = description;
            Progress = progress;
            CreatorId = creatorId;
        }

        public void UpdateId(string id)
        {
            Id = id;
        }
        public void UpdateName(string name)
        {
            Name = name;
        }
        public void UpdateDescription(string des)
        {
            Description = des;
        }
        public void UpdateProgress(int i)
        {
            Progress = i;
        }

        public void UpdateTags(IList<Tag> list)
        {
            _tags.Clear();
            _tags.AddRange(list);
        }

    }
}
