using Bug.API.Dto;
using Bug.Data.Infrastructure;
using Bug.Entities.Model;
using Bug.Data.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
                .GetEntityAsync(specification, cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetRolesByProjectAsync
            (string projectId,            
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RoleByProjectSpecification(projectId);
            return await _unitOfWork
                .Role
                .GetAllEntitiesAsync(specificationResult, cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetRolesByCreatorAsync
            (string creatorId,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RoleByCreatorSpecification(creatorId);
            return await _unitOfWork
                .Role
                .GetAllEntitiesAsync(specificationResult, cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetRolesWhichMemberOn
            (string projectId,
            string memberId,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RolesWhichMemberOnSpecification(projectId, memberId);
            return await _unitOfWork
                .Role
                .GetAllEntitiesAsync(specificationResult, cancellationToken);
        }

        public async Task<Role> AddNewRoleAsync
            (RoleNormalDto role,
            CancellationToken cancellationToken = default)
        {
            var result = new Role(role.Name, role.Description, role.CreatorId);
            await _unitOfWork
                .Role
                .AddAsync(result, cancellationToken);
            if(!string.IsNullOrEmpty(role.ProjectId))
            {
                var p = await _unitOfWork
                    .Project
                    .GetByIdAsync(role.ProjectId, cancellationToken);
                p?.AddExistRole(result);
            }
            await _unitOfWork.SaveAsync(cancellationToken);
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
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task DeleteRoleAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Role.GetByIdAsync(id, cancellationToken);
            _unitOfWork.Role.Delete(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
