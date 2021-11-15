using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Attachment : IEntityBase
    {
        public int Id { get; private set; }
        public string Uri { get; private set; }
        public string IssueId { get; private set; }
        public Issue Issue { get; private set; }

        private Attachment() { }
        public Attachment(int id, string uri, string issueId)
        {
            Id = id;
            Uri = uri;
            IssueId = issueId;
        }
    }
}
