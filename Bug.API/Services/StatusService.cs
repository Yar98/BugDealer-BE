using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bug.Data.Infrastructure;
using Bug.Entities.Model;
using Bug.Data.Specifications;
using Bug.API.Dto;

namespace Bug.API.Services
{
    public class StatusService : IStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Status> GetStatusDetailByIdAsync
            (string id,
            CancellationToken cancellationToken = default)
        {
            StatusDetailLv1Specification specificationResult =
                new(id);
            return await _unitOfWork
                .Status
                .GetStatusAsync(specificationResult, cancellationToken);
        }

        public async Task<PaginatedListDto<Status>> GetPaginatedDetailByCreatorAsync
            (string creatorId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            StatusDetailLv1Specification specificationResult =
                new(creatorId);
            var result = await _unitOfWork
                .Status
                .GetPaginatedIssuesAsync(pageIndex, pageSize, sortOrder, specificationResult,cancellationToken);
            return new PaginatedListDto<Status>
            {
                Length = result.Length,
                Items = result
            };
        }
        public async Task<IReadOnlyList<Status>> GetNextByOffsetDetailByCreatorAsync
            (string creatorId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            StatusDetailLv1Specification specificationResult =
                new(creatorId);
            var result = await _unitOfWork
                .Status
                .GetNextIssuesByOffsetAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }
        public async Task<Status> AddStatusAsync
            (StatusNormalDto status,
            CancellationToken cancellationToken = default)
        {
            var result = new Status(Guid.NewGuid().ToString(),
                status.Name,
                status.Description,
                status.Progress);
            result.UpdateAccounts(status.Accounts);
            result.UpdateTags(status.Tags);
            return await _unitOfWork
                .Status
                .AddAsync(result, cancellationToken);
        }
        public async Task UpdateStatusAsync
            (StatusNormalDto status,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Status.GetByIdAsync(status.Id, cancellationToken);
            result.UpdateName(status.Name);
            result.UpdateDescription(status.Description);
            result.UpdateProgress(status.Progress);
            result.UpdateAccounts(status.Accounts);
            result.UpdateTags(status.Tags);
            await _unitOfWork.Status.UpdateAsync(result, cancellationToken);
        }
        public async Task DeleteStatusAsync
            (string statusId,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Status.GetByIdAsync(statusId, cancellationToken);
            await _unitOfWork.Status.DeleteAsync(result, cancellationToken);
        }
    }
}
