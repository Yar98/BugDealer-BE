using Bug.Data.Infrastructure;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bug.Data.Specifications;
using Bug.API.Dto;

namespace Bug.API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedListDto<Notification>> GetPaginatedByByAccountIdAsync
            (string accountId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new NotificationsByAccountId(accountId);
            var result = await _unitOfWork
                .Notification
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Notification>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Notification>> GetNextByOffsetByAccountIdAsync
            (string accountId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new NotificationsByAccountId(accountId);
            var result = await _unitOfWork
                .Notification
                .GetNextByOffsetNoTrackBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<PaginatedListDto<Notification>> GetPaginatedByByAccountIdSeenAsync
            (string accountId,
            bool seen,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new NotificationsByAccountId(accountId,seen);
            var result = await _unitOfWork
                .Notification
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Notification>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Notification>> GetNextByOffsetByAccountIdSeenAsync
            (string accountId,
            bool seen,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new NotificationsByAccountId(accountId,seen);
            var result = await _unitOfWork
                .Notification
                .GetNextByOffsetNoTrackBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }
    }
}
