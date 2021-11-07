using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bug.API.Dto;
using Bug.Core.Utility;
using Bug.Entities.Model;

namespace Bug.API.Services
{
    public interface IProjectService
    {
        Task<Project> GetDetailProjectAsync
            (string id,
            CancellationToken cancellationToken = default);
        Task<ProjectNormalDto> GetNormalProjectAsync
            (string projectId,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<ProjectNormalDto>> GetPaginatedByCreatorAsync
            (string creatorId,
            int pageIndex, 
            int pageSize,
            int categoryId, 
            string tagName,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<ProjectNormalDto>> GetPaginatedByMemberAsync
            (string creatorId,
            int pageIndex, 
            int pageSize,
            int categoryId, 
            string tagName,
            string sortOrder,
            CancellationToken cancellation = default);
        Task<IReadOnlyList<ProjectNormalDto>> GetNextByOffsetByCreatorAsync
            (string creatorId,
            int offset, 
            int next,
            int categoryId, 
            string tagName,
            string sortOrder,
            CancellationToken cancellation = default);
        Task<IReadOnlyList<ProjectNormalDto>> GetNextByOffsetByMemberAsync
            (string creatorId,
            int offset, 
            int next,
            int categoryId, 
            string tagName,
            string sortOrder,
            CancellationToken cancellation = default);
        Task<Project> AddProjectAsync
            (ProjectNormalDto pro,
            CancellationToken cancellationToken = default);
        Task UpdateProjectAsync
            (ProjectNormalDto pro,
            CancellationToken cancellation = default);
        Task DeleteProjectAsync
            (string projectId,
            CancellationToken cancellation = default);
    }
}
