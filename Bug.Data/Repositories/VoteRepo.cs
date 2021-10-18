using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public class VoteRepo : EntityRepoBase<Vote>, IVoteRepo
    {
        public VoteRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
