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
        //Task<IReadOnlyList<ProjectLowDto>> GetRecentProjects(
        //    string accountId, int categoryId, string tagName, int count);
        Task<ProjectNormalDto> AddProject
            (ProjectNormalDto pro,
            CancellationToken cancellationToken = default);
        Task<Project> GetDetailProject
            (string id,
            CancellationToken cancellationToken = default);
        Task<ProjectNormalDto> GetNormalProject
            (string projectId,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<ProjectNormalDto>> GetPaginatedByCreator
            (string creatorId,
            int pageIndex, 
            int pageSize,
            int categoryId, 
            string tagName,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<ProjectNormalDto>> GetPaginatedByMember
            (string creatorId,
            int pageIndex, 
            int pageSize,
            int categoryId, 
            string tagName,
            string sortOrder,
            CancellationToken cancellation = default);
        Task<IReadOnlyList<ProjectNormalDto>> GetNextByOffsetByCreator(
            string creatorId,
            int offset, 
            int next,
            int categoryId, 
            string tagName,
            string sortOrder,
            CancellationToken cancellation = default);
        Task<IReadOnlyList<ProjectNormalDto>> GetNextByOffsetByMember(
            string creatorId,
            int offset, 
            int next,
            int categoryId, 
            string tagName,
            string sortOrder,
            CancellationToken cancellation = default);
        Task UpdateProject
            (ProjectNormalDto pro,
            CancellationToken cancellation = default);
        Task DeleteProject
            (string projectId,
            CancellationToken cancellation = default);
    }
}
