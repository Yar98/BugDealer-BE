using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public class RoleRepo : EntityRepoBase<Role>, IRoleRepo
    {
        public RoleRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
