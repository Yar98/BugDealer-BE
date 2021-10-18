using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public class CommentRepo : EntityRepoBase<Comment>, ICommentRepo
    {
        public CommentRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
