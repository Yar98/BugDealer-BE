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
        Task<Project> GetNormalProjectAsync
            (string projectId,
            CancellationToken cancellationToken = default);
        Task<Project> GetDetailProjectAsync
            (string id,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Project>> GetAllWhichAccountIdJoin
            (string accountId,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Projectlog>> GetNextRecentByOffsetAsync
           (string accountId,
           int offset,
           int next,
           CancellationToken cancellationToken = default);
        Task<Project> GetProjectsByCodeCreatorId
            (string creatorId,
            string code,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Project>> GetPaginatedByMemberIdSearchAsync
            (string accountId,
            int state,
            string search,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Project>> GetPaginatedByCreatorIdStatusAsync
            (string creatorId,
            int pageIndex, 
            int pageSize,
            int tagId,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Project>> GetPaginatedByMemberIdStateAsync
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
        Task<IReadOnlyList<Project>> GetNextByOffsetByMemberIdStateAsync
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
            (ProjectPutStatusesDto pro,
            CancellationToken cancellationToken = default);
        Task AddMemberToProjectAsync
            (string memberId,
            string projectId,
            CancellationToken cancellationToken = default);
        Task AddRoleToProjectAsync
            (string projectId,
            int roleId,
            CancellationToken cancellationToken = default);
        Task DeleteMemberFromProjectAsync
            (string projectId,
            string accountId,
            CancellationToken cancellationToken = default);
        Task DeleteProjectAsync
            (string projectId,
            CancellationToken cancellation = default);
    }
}
