﻿using Bug.API.Dto;
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
        Task<PermissionByCategoryDto> GetAllAsync
            (CancellationToken cancellationToken = default);
        Task<IReadOnlyList<PermissionNormalDto>> GetPermissionsByRoleProjectAsync
            (int roleId,
            string projectId,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<PermissionNormalDto>> GetPermissionsByAccountProjectAsync
            (string accountId,
            string projectId,
            string sortOrder,
            CancellationToken cancellationToken = default);
    }
}
