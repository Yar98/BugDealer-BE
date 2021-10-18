using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public class RelationRepo : EntityRepoBase<Relation>, IRelationRepo
    {
        public RelationRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
