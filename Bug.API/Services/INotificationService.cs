using Bug.API.Dto;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface INotificationService
    {
        Task<PaginatedListDto<Notification>> GetPaginatedByByAccountIdAsync
            (string accountId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Notification>> GetNextByOffsetByAccountIdAsync
            (string accountId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Notification>> GetPaginatedByByAccountIdSeenAsync
            (string accountId,
            bool seen,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Notification>> GetNextByOffsetByAccountIdSeenAsync
            (string accountId,
            bool seen,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
    }
}
