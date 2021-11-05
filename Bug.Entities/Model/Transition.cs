using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Transition : IEntityBase 
    {
        public string Id { get; private set; }
        public string WorkflowId { get; private set; }
        public Workflow Workflow { get; private set; }
        //public string StartStatusId { get; private set; }
        //public Status StartStatus { get; private set; }

        //public string? EndStatusId { get; private set; }
        //public Status EndStatus { get; private set; }

        //public ICollection<Role> Roles { get; private set; }
        private Transition() { }
        public Transition(string id,
            string workflowId,
            string startStatusId,
            string endStatusId)
        {
            Id = id;
            WorkflowId = workflowId;
            //StartStatusId = startStatusId;
            //EndStatusId = endStatusId;
        }
    }
}
