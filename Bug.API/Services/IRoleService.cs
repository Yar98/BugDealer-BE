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
        Task<IReadOnlyList<Role>> GetRolesByProjectAsync
            (string projectId,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Role>> GetRolesByCreatorAsync
            (string creatorId,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Role>> GetRolesWhichMemberOn
            (string projectId,
            string memberId,
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
