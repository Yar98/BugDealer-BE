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
        Task<PaginatedListDto<Status>> GetPaginatedDetailByCreatorIdAsync
            (string creatorId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Status>> GetNextByOffsetDetailByCreatorIdAsync
            (string creatorId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Status>> GetPaginatedDetailByCreatorIdProjectIdAsync
            (string projectId,
            string creatorId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Status>> GetNextByOffsetDetailByCreatorIdProjectIdAsync
            (string projectId,
            string creatorId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Status>> GetStatusesByCreatorIdAsync
           (string creatorId,
           CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Status>> GetStatusesByProjectIdAsync
            (string projectId,
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
