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
        Task<IReadOnlyList<Status>> GetDefaultStatusesNoTrackAsync
            (string creatorId = "bts",
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Status>> GetDefaultStatusesAsync
            (string creatorId = "bts",
            CancellationToken cancellationToken = default);
    }
}
