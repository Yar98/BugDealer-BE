using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface ITagService
    {
        Task<Tag> GetTagByIdAsync
            (int id,
            CancellationToken cancellationToken = default);
        Task<Tag> GetDetailTagByIdAsync
            (int id,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Tag>> GetTagsByCategoryIdAsync
            (int id,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Tag>> GetTagsByCategoryIdProjectIdAsync
            (string projectId,
            int id,
            CancellationToken cancellationToken = default);
    }
}
