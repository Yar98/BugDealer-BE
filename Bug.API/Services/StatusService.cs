using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bug.Data.Infrastructure;
using Bug.Entities.Model;
using Bug.Data.Specifications;
using Bug.API.Dto;
using Bug.API.BtsException;

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
            var specificationResult =
                new StatusSpecification(id);
            return await _unitOfWork
                .Status
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
        }

        public async Task<IReadOnlyList<Status>> GetAllByProjectId
           (string projectId,
            string sortOrder,
           CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new StatusesByProjectIdSpecification(projectId);
            return await _unitOfWork
                .Status
                .GetAllEntitiesNoTrackBySpecAsync(specificationResult, sortOrder, cancellationToken);
        }

        public async Task<IReadOnlyList<Status>> GetAllBtsStatuses
            (string sortOrder,
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork
                .Status
                .GetDefaultStatusesNoTrackAsync(sortOrder, "bts", cancellationToken);
        }

        public async Task<PaginatedListDto<Status>> GetPaginatedByProjectIdSearch
            (string projectId,
            string search,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new StatusesByProjectIdSearchSpecification(projectId, search);
            var result = await _unitOfWork
                .Status
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Status>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<PaginatedListDto<Status>> GetPaginatedByCreatorIdSearch
            (string creatorId,
            string search,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new StatusesByCreatorIdSearchSpecification(creatorId, search);
            var result = await _unitOfWork
                .Status
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Status>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<PaginatedListDto<Status>> GetPaginatedDetailByCreatorIdProjectIdAsync
            (string projectId,
            string creatorId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new StatusesByProjectIdSpecification(projectId);
            var result = await _unitOfWork
                .Status
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Status>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Status>> GetNextByOffsetDetailByCreatorIdProjectIdAsync
            (string projectId,
            string creatorId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new StatusesByProjectIdSpecification(projectId);
            var result = await _unitOfWork
                .Status
                .GetNextByOffsetNoTrackBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<PaginatedListDto<Status>> GetPaginatedDetailByCreatorIdAsync
            (string creatorId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new StatusesByCreatorIdSpecification(creatorId);
            var result = await _unitOfWork
                .Status
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult,cancellationToken);
            return new PaginatedListDto<Status>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Status>> GetNextByOffsetDetailByCreatorIdAsync
            (string creatorId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new StatusesByCreatorIdSpecification(creatorId);
            var result = await _unitOfWork
                .Status
                .GetNextByOffsetNoTrackBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<IReadOnlyList<Status>> GetStatusesByCreatorIdAsync
            (string creatorId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new StatusesByCreatorIdSpecification(creatorId);
            return await _unitOfWork
                .Status
                .GetAllEntitiesNoTrackBySpecAsync(specificationResult, sortOrder, cancellationToken);
        }

        public async Task<IReadOnlyList<Status>> GetStatusesExceptBtsByProjectIdAsync
            (string projectId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new StatusesByProjectIdSpecification(projectId);
            var result = await _unitOfWork
                .Status
                .GetAllEntitiesNoTrackBySpecAsync(specificationResult, sortOrder, cancellationToken);
            
            return result
                .Where(st => st.CreatorId != "bts")
                .ToList();          
        }

        public async Task<Status> AddStatusAsync
            (StatusNormalDto status,
            CancellationToken cancellationToken = default)
        {
            var result = new Status(Guid.NewGuid().ToString(),
                status.Name,
                status.Description,
                status.Progress??0,
                status.CreatorId,
                status.TagId??1);
            await _unitOfWork
                .Status
                .AddAsync(result, cancellationToken);
            _unitOfWork.Save();
            return result;
        }

        public async Task UpdateStatusAsync
            (StatusNormalDto status,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Status
                .GetByIdAsync(status.Id, cancellationToken);
            if(status.Name != null)
                result.UpdateName(status.Name);
            if(status.Description != null)
                result.UpdateDescription(status.Description);
            if(status.Progress != null)
                result.UpdateProgress(status.Progress??0);
            if(status.CreatorId != null)
                result.UpdateCreatorId(status.CreatorId);
            if(status.TagId != null)
                result.UpdateTagId(status.TagId??1);

            _unitOfWork.Status.Update(result);

            _unitOfWork.Save();
        }

        public async Task DeleteStatusAsync
            (string statusId,
            CancellationToken cancellationToken = default)
        {
            var defaultStatuses = await _unitOfWork
                .Status
                .GetDefaultStatusesNoTrackAsync("",cancellationToken: cancellationToken);
            if (defaultStatuses.Any(r => r.Id == statusId))
                throw new CannotDeleteDefault();
            var result = await _unitOfWork
                .Status
                .GetEntityBySpecAsync(new StatusSpecification(statusId), cancellationToken);
            if (result.Projects.Count != 0)
                throw new CannotDeleteStatusInUse();
            _unitOfWork
                .Issue
                .UpdateIssuesHaveDumbStatus(new List<Status> { result });
            _unitOfWork.Status.Delete(result);
            
            _unitOfWork.Save();
        }
    }
}
