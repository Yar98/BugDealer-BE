using Bug.API.Dto;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface IRoleService
    {
        Task<Role> GetDetailRoleByIdAsync
            (string id,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Role>> GetPaginatedDetailByProjectAsync
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Role>> GetNextDetailByOffsetByProjectAsync
            (string projectId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<Role> AddRoleAsync
            (RoleNormalDto role,
            CancellationToken cancellationToken = default);
        Task UpdateRoleAsync
            (RoleNormalDto role,
            CancellationToken cancellationToken = default);
        Task DeleteRoleAsync
            (string id,
            CancellationToken cancellationToken = default);
    }
}
