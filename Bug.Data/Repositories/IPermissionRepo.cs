using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public interface IPermissionRepo : IEntityRepoBase<Permission>
    {
        Task<List<Permission>> GetPermissionsFromMutiIdsAsync
            (List<int> list,
            CancellationToken cancellationToken = default);
    }
}
