using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Repositories
{
    public class FieldRepo : EntityRepoBase<Field>, IFieldRepo
    {
        public FieldRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }
        public override IQueryable<Field> SortOrder
            (IQueryable<Field> result, string sortOrder)
        {
            return null;
        }
    }
}
