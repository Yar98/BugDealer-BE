using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Repositories
{
    public class AttachmentRepo : EntityRepoBase<Attachment>, IAttachmentRepo
    {
        public AttachmentRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public override IQueryable<Attachment> SortOrder(IQueryable<Attachment> result, string sortOrder)
        {
            return null;
        }
    }
}
