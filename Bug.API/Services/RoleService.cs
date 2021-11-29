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
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RoleByProjectSpecification(projectId);
            return await _unitOfWork
                .Role
                .GetAllEntitiesBySpecAsync(specificationResult, cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetRolesByCreatorIdAsync
            (string creatorId,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RoleByCreatorIdSpecification(creatorId);
            return await _unitOfWork
                .Role
                .GetAllEntitiesBySpecAsync(specificationResult, cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetRolesWhichMemberIdOnAsync
            (string projectId,
            string memberId,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RolesWhichMemberOnSpecification(projectId, memberId);
            return await _unitOfWork
                .Role
                .GetAllEntitiesBySpecAsync(specificationResult, cancellationToken);
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
            var ps = role
                .Permissions
                .Select(p=>new Permission(p.Id,p.Action))
                .ToList();
            result.UpdatePermission(ps);
            
            _unitOfWork.Save();
            return result;
        }

        public async Task UpdateRoleAsync
            (RoleNormalDto role,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Role.GetByIdAsync(role.Id,cancellationToken);
            result.UpdateCreatorId(role.CreatorId);
            result.UpdateDescription(role.Description);
            result.UpdateName(role.Name);
            _unitOfWork.Role.Update(result);
            _unitOfWork.Save();
        }

        public async Task DeleteRoleAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            var defaultRoles = await _unitOfWork
                .Role
                .GetDefaultRolesNoTrackAsync(cancellationToken:cancellationToken);
            if(defaultRoles.Any(r=>r.Id == id))
            {
                throw new CannotDeleteDefault();
            }    
            var result = await _unitOfWork
                .Role
                .GetByIdAsync(id, cancellationToken);
            _unitOfWork.Role.Delete(result);
            _unitOfWork.Save();
        }
    }
}
