using Bug.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Entities.Model;
using System.Threading;
using Bug.Data.Specifications;
using Bug.API.Dto;

namespace Bug.API.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Permission> GetPermissionByIdAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork
                .Permission
                .GetByIdAsync(id, cancellationToken);
        }

        public async Task<PermissionByCategoryDto> GetAllAsync
            (CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Permission
                .FindAllAsync(cancellationToken);
            return new PermissionByCategoryDto
            {
                ProjectPermissions = result.Where(p => p.CategoryId == 5).ToList(),
                IssueTrackingPermissions = result.Where(p => p.CategoryId == 6).ToList()
            };
        }

        public async Task<IReadOnlyList<PermissionNormalDto>> GetPermissionsByRoleProjectAsync
            (int roleId,
            string projectId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new PermissionsByRoleProjectSpecification(roleId, projectId);
            var activePermissions = await _unitOfWork
                .Permission
                .GetAllEntitiesBySpecAsync(specificationResult, sortOrder, cancellationToken);
            var permissions = await _unitOfWork
                .Permission
                .FindAllAsync(cancellationToken);
            return permissions
                .Select(p => new PermissionNormalDto
                {
                    Id = p.Id,
                    Action = p.Action,
                    Active = activePermissions.Contains(p)
                })
                .ToList();
        }
        public async Task<IReadOnlyList<PermissionNormalDto>> GetPermissionsByAccountProjectAsync
            (string accountId,
            string projectId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new PermissionsByAccountProjectSpecification(accountId, projectId);
            var activePermissions = await _unitOfWork
                .Permission
                .GetAllEntitiesBySpecAsync(specificationResult, sortOrder, cancellationToken);
            var permissions = await _unitOfWork
                .Permission
                .FindAllAsync(cancellationToken);
            return permissions
                .Select(p => new PermissionNormalDto
                {
                    Id = p.Id,
                    Action = p.Action,
                    Active = activePermissions.Contains(p)
                })
                .ToList();
        }
    }
}
