using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class NotificationsByAccountId : BaseSpecification<Notification>
    {
        public NotificationsByAccountId(string accountId)
            : base(n=>n.AccountId == accountId)
        {
            AddInclude(n => n.Account);
            AddInclude(n => n.Issuelog);
        }

        public NotificationsByAccountId(string accountId, bool seen)
            :base(n=>n.AccountId == accountId && n.Seen == seen)
        {
            AddInclude(n => n.Account);
            AddInclude(n => n.Issuelog);
        }
    }
}
