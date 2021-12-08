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
        Task<ProjectPostDto> GetNormalProjectAsync
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
            (ProjectPostDto pro,
            CancellationToken cancellationToken = default);
        Task UpdateBasicProjectAsync
            (ProjectPutDto pro,
            CancellationToken cancellation = default);
        Task UpdateRolesOfProjectAsync
            (ProjectPutDto pro,
            CancellationToken cancellationToken = default);        
        Task UpdateStatusesOfProjectAsync
            (ProjectPutDto pro,
            CancellationToken cancellationToken = default);
        Task AddMemberToProjectAsync
            (string memberId,
            string projectId,
            CancellationToken cancellationToken = default);
        Task AddRoleToProjectAsync
            (string projectId,
            int roleId,
            CancellationToken cancellationToken = default);
        Task DeleteProjectAsync
            (string projectId,
            CancellationToken cancellation = default);
    }
}
