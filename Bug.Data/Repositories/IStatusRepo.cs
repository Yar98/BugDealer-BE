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
    public interface IStatusRepo : IEntityRepoBase<Status>
    {
        Task<List<Status>> GetStatusesFromMutiIdsAsync
            (List<string> list,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Status>> GetDefaultStatusesNoTrackAsync
            (string sortOrder,
            string creatorId = "bts",
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Status>> GetDefaultStatusesAsync
            (string sortOrder,
            string creatorId = "bts",
            CancellationToken cancellationToken = default);
    }
}
