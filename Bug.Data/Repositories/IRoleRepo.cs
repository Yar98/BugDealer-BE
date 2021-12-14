using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Core.Utils;
using Bug.Data.Specifications;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public interface IRoleRepo : IEntityRepoBase<Role>
    {
        Task<List<Role>> GetRolesFromMutiIdsAsync
            (List<int> list,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Role>> GetDefaultRolesNoTrackAsync
            (string sortOrder, 
            string creatorId = "bts",
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Role>> GetDefaultRolesAsync
            (string sortOrder, 
            string creatorId = "bts",
            CancellationToken cancellationToken = default);

    }
}
