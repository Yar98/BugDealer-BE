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
        public async Task<Status> GetDetailStatusByIdAsync
            (string id,
            CancellationToken cancellationToken = default)
        {
            StatusDetailLv1Specification specificationResult =
                new(id);
            return await _unitOfWork
                .Status
                .GetEntityAsync(specificationResult, cancellationToken);
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
                .GetPaginatedAsync(pageIndex, pageSize, sortOrder, specificationResult,cancellationToken);
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
                .GetNextByOffsetAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }
        public async Task<Status> AddStatusAsync
            (StatusNormalDto status,
            CancellationToken cancellationToken = default)
        {
            var result = new Status(Guid.NewGuid().ToString(),
                status.Name,
                status.Description,
                status.Progress,
                status.CreatorId);
            result.UpdateAccounts(status.Accounts);
            result.UpdateTags(status.Tags);
            await _unitOfWork
                .Status
                .AddAsync(result, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return result;
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
            _unitOfWork.Status.Update(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
        public async Task DeleteStatusAsync
            (string statusId,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Status.GetByIdAsync(statusId, cancellationToken);
            _unitOfWork.Status.Delete(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
