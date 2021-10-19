using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public class IssuelogRepo : EntityRepoBase<Issuelog>, IIssuelogRepo
    {
        public IssuelogRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
