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

        private readonly List<Tag> _tags = new List<Tag>();
        public ICollection<Tag> Tags => _tags.AsReadOnly();

        private readonly List<Account> _accounts = new List<Account>();
        public ICollection<Account> Accounts => _accounts.AsReadOnly();

        //public ICollection<Workflow> Workflows { get; private set; }
        private Status() { }
        public Status(string id,
            string name,
            string description,
            int progress)
        {
            Id = id;
            Name = name;
            Description = description;
            Progress = progress;
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
        public void UpdateAccounts(IList<Account> list)
        {
            _accounts.Clear();
            _accounts.AddRange(list);
        }
        public void AddAccount(Account acc)
        {
            if (!Accounts.Any(a => a.Id == acc.Id))
            {
                _accounts.Add(acc);
                return;
            }
        }
    }
}
