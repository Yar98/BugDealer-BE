using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.Data.Repositories
{
    public interface IProjectlogRepo : IEntityRepoBase<Projectlog>
    {
        Task<IReadOnlyList<Projectlog>> GetRecentAsync
            (string accountId,
            int offset,
            int next,
            CancellationToken cancellationToken = default);
    }
}
