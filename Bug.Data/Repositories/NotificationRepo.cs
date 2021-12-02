using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Repositories
{
    public class NotificationRepo : EntityRepoBase<Notification>, INotificationRepo
    {
        public NotificationRepo(BugContext bugContext)
            : base(bugContext)
        {

        }

        public override IQueryable<Notification> SortOrder(IQueryable<Notification> result, string sortOrder)
        {
            throw new NotImplementedException();
        }
    }
}
