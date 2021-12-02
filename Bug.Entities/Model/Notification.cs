using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Notification : IEntityBase
    {
        public int Id { get; private set; }
        public bool Seen { get; private set; }
        public int IssuelogId { get; private set; }
        public Issuelog Issuelog { get; private set; }
        public string AccountId { get; private set; }
        public Account Account { get; private set; }

        private Notification() { }

        public Notification
            (int id,
            int issuelogId,
            string accountId)
        {
            Id = id;
            IssuelogId = issuelogId;
            AccountId = accountId;
        }

        public void UpdateSeen(bool s)
        {
            Seen = s;
        }
    }
}
