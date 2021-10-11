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
        public int TagId { get; private set; }
        public string AccountId { get; private set; }
        public Account Account { get; private set; }
        public ICollection<Tag> Tags { get; private set; }
        public ICollection<Workflow> Workflows { get; private set; }
        private Status() { }
        public Status(string id,
            string name,
            string description,
            int progress,
            int categoryId,
            string accountId)
        {
            Id = id;
            Name = name;
            Description = description;
            Progress = progress;
            TagId = categoryId;
            AccountId = accountId;
        }
    }
}
