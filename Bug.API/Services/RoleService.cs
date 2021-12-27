using Bug.API.Dto;
using Bug.Data.Infrastructure;
using Bug.Entities.Model;
using Bug.Data.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bug.API.BtsException;

namespace Bug.API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Role> GetDetailRoleByIdAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            RoleSpecification specification =
                new(id);
            return await _unitOfWork.Role
                .GetEntityBySpecAsync(specification, cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetRolesByProjectIdAsync
            (string projectId,     
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RoleByProjectSpecification(projectId);
            return await _unitOfWork
                .Role
                .GetAllEntitiesNoTrackBySpecAsync(specificationResult, sortOrder, cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetRolesByCreatorIdAsync
            (string creatorId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RoleByCreatorIdSpecification(creatorId);
            return await _unitOfWork
                .Role
                .GetAllEntitiesNoTrackBySpecAsync(specificationResult, sortOrder, cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetRolesWhichMemberIdProjectIdOnAsync
            (string projectId,
            string memberId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RolesWhichMemberOnSpecification(projectId, memberId);
            return await _unitOfWork
                .Role
                .GetAllEntitiesNoTrackBySpecAsync(specificationResult, sortOrder, cancellationToken);
        }

        public async Task<PaginatedListDto<Role>> GetPaginatedByProjectIdSearch
            (string projectId,
            string search,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RolesByProjectIdSearchSpecification(projectId, search);
            var result = await _unitOfWork
                .Role
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Role>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<PaginatedListDto<Role>> GetPaginatedByCreatorIdSearch
            (string creatorId,
            string search,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RolesByCreatorIdSearchSpecification(creatorId, search);
            var result = await _unitOfWork
                .Role
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Role>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<PaginatedListDto<Role>> GetPaginatedProjectIdAsync
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RoleByProjectSpecification(projectId);
            var result = await _unitOfWork
                .Role
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Role>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<PaginatedListDto<Role>> GetPaginatedWhichMemberIdOnAsync
            (string projectId,
            string memberId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RolesWhichMemberOnSpecification(projectId, memberId);
            var result = await _unitOfWork
                .Role
                .GetPaginatedNoTrackBySpecAsync(pageIndex,pageSize,sortOrder,specificationResult, cancellationToken);
            return new PaginatedListDto<Role>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Role>> GetNextByOffsetWhichMemberIdOnAsync
            (string projectId,
            string memberId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RolesWhichMemberOnSpecification(projectId, memberId);
            var result = await _unitOfWork
                .Role
                .GetNextByOffsetNoTrackBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<PaginatedListDto<Role>> GetPaginatedByCreatorId
            (string accountId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RoleByCreatorIdSpecification(accountId);
            var result = await _unitOfWork
                .Role
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);

            return new PaginatedListDto<Role>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Role>> GetNextByOffsetByCreatorIdAsync
            (string accountId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RoleByCreatorIdSpecification(accountId);
            var result = await _unitOfWork
                .Role
                .GetNextByOffsetNoTrackBySpecAsync(offset,next,sortOrder,specificationResult,cancellationToken);
            
            return result;
        }

        public async Task<Role> AddNewRoleAsync
            (RoleNormalDto role,
            CancellationToken cancellationToken = default)
        {
            var result = new Role(0, role.Name, role.Description, role.CreatorId);
            await _unitOfWork
                .Role
                .AddAsync(result, cancellationToken);           
            var ps = await _unitOfWork
                .Permission
                .GetPermissionsFromMutiIdsAsync(role.Permissions.Select(p=>p.Id).ToList(),cancellationToken);
            result.UpdatePermission(ps);
            
            _unitOfWork.Save();
            return result;
        }

        public async Task UpdateRoleAsync
            (RoleNormalDto role,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Role
                .GetEntityBySpecAsync(new RoleSpecification(role.Id),cancellationToken);
            if(role.CreatorId != null)
                result.UpdateCreatorId(role.CreatorId);
            if(role.Description != null)
                result.UpdateDescription(role.Description);
            if(role.Name != null)
                result.UpdateName(role.Name);
            var ps = await _unitOfWork
               .Permission
               .GetPermissionsFromMutiIdsAsync(role.Permissions.Select(p => p.Id).ToList(), cancellationToken);
            result.UpdatePermission(ps??new List<Permission>());

            _unitOfWork.Save();
        }

        public async Task DeleteRoleAsync
            (int id,
            CancellationToken cancellationToken = default)
        {            
            var defaultRoles = await _unitOfWork
                .Role
                .GetDefaultRolesNoTrackAsync(null,cancellationToken:cancellationToken);
            if(defaultRoles.Any(r=>r.Id == id))
                throw new CannotDeleteDefault();

            var result = await _unitOfWork
                .Role
                .GetByIdAsync(id, cancellationToken);
            if (result.Projects.Count != 0)
                throw new CannotDeleteRoleInUse();

            //var check = _unitOfWork
                //.AccountProjectRole
                //.UpdateAprBeforeDeleteRole(id);

            _unitOfWork.Role.Delete(result);

            _unitOfWork.Save();
        }

    }
}
