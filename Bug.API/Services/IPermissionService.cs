using Bug.API.Dto;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface IPermissionService
    {
        Task<Permission> GetPermissionByIdAsync
            (int id,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Permission>> GetAllAsync
            (CancellationToken cancellationToken = default);
        Task<IReadOnlyList<PermissionNormalDto>> GetPermissionsByRoleProjectAsync
            (string roleId,
            string projectId,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<PermissionNormalDto>> GetPermissionsByAccountProjectAsync
            (string roleId,
            string projectId,
            CancellationToken cancellationToken = default);
    }
}
