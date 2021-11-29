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
            (int id,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Role>> GetRolesByProjectIdAsync
            (string projectId,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Role>> GetRolesByCreatorIdAsync
            (string creatorId,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Role>> GetRolesWhichMemberIdOnAsync
            (string projectId,
            string memberId,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Role>> GetPaginatedByCreatorId
            (string accountId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Role>> GetNextByOffsetByCreatorIdAsync
            (string accountId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<Role> AddNewRoleAsync
            (RoleNormalDto role,
            CancellationToken cancellationToken = default);
        Task UpdateRoleAsync
            (RoleNormalDto role,
            CancellationToken cancellationToken = default);
        Task DeleteRoleAsync
            (int id,
            CancellationToken cancellationToken = default);
    }
}
