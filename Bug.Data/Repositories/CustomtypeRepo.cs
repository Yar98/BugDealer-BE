using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Repositories
{
    public class CustomtypeRepo : EntityRepoBase<Customtype>, ICustomtypeRepo
    {
        public CustomtypeRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public override IQueryable<Customtype> SortOrder(IQueryable<Customtype> result, string sortOrder)
        {
            throw new NotImplementedException();
        }
    }
}
