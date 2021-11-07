using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Workflow : IEntityBase
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        //public string AccountId { get; private set; }
        //public Account Account { get; private set; }
        //public ICollection<Status> Statuses { get; private set; }
        private Workflow() { }
        public Workflow(string id,
            string name,
            string accountId)
        {
            Id = id;
            Name = name;
            //AccountId = accountId;
        }
    }
}
