using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Repositories
{
    public class SeverityRepo : EntityRepoBase<Severity>, ISeverityRepo
    {
        public SeverityRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public override IQueryable<Severity> SortOrder(IQueryable<Severity> result, string sortOrder)
        {
            throw new NotImplementedException();
        }
    }
}
