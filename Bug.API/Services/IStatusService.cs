using Bug.API.Dto;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface IStatusService
    {
        Task<Status> GetDetailStatusByIdAsync
            (string id,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Status>> GetPaginatedDetailByCreatorAsync
            (string creatorId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Status>> GetNextByOffsetDetailByCreatorAsync
            (string creatorId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<Status> AddStatusAsync
            (StatusNormalDto status,
            CancellationToken cancellationToken = default);
        Task UpdateStatusAsync
            (StatusNormalDto status,
            CancellationToken cancellationToken = default);
        Task DeleteStatusAsync
            (string statusId,
            CancellationToken cancellationToken = default);
    }
}
