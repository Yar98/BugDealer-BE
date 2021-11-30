using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bug.API.Dto;
using Bug.Core.Utils;
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
        Task<PaginatedListDto<Project>> GetPaginatedByCreatorIdStatusAsync
            (string creatorId,
            int pageIndex, 
            int pageSize,
            int tagId,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Project>> GetPaginatedByMemberIdTagIdAsync
            (string creatorId,
            int pageIndex, 
            int pageSize,
            int tagId,
            string sortOrder,
            CancellationToken cancellation = default);
        Task<IReadOnlyList<Project>> GetNextByOffsetByCreatorIdTagIdAsync
            (string creatorId,
            int offset,
            int next,
            int tagId,
            string sortOrder,
            CancellationToken cancellation = default);
        Task<IReadOnlyList<Project>> GetNextByOffsetByMemberIdTagIdAsync
            (string creatorId,
            int offset, 
            int next,
            int tagId,
            string sortOrder,
            CancellationToken cancellation = default);
        Task<Project> AddProjectAsync
            (ProjectNormalDto pro,
            CancellationToken cancellationToken = default);
        Task UpdateBasicProjectAsync
            (ProjectNormalDto pro,
            CancellationToken cancellation = default);
        Task UpdateRolesOfProjectAsync
            (ProjectNormalDto pro,
            CancellationToken cancellationToken = default);
        Task AddRoleToProjectAsync
            (string projectId,
            int roleId,
            CancellationToken cancellationToken = default);
        Task UpdateStatusesOfProjectAsync
            (ProjectNormalDto pro,
            CancellationToken cancellationToken = default);
        Task DeleteProjectAsync
            (string projectId,
            CancellationToken cancellation = default);
    }
}
