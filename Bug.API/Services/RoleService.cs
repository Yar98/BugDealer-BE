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
            (string id,
            CancellationToken cancellationToken = default)
        {
            RoleDetailLv1Specification specification =
                new(id);
            return await _unitOfWork.Role
                .GetRoleAsync(specification, cancellationToken);
        }

        public async Task<PaginatedListDto<Role>> GetPaginatedDetailByProjectAsync
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            RoleDetailLv1ByProjectSpecification specificationResult =
                new(projectId);
            var result = await _unitOfWork
                .Role
                .GetPaginatedAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Role>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Role>> GetNextDetailByOffsetByProjectAsync
            (string projectId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            RoleDetailLv1ByProjectSpecification specificationResult =
                new(projectId);
            var result = await _unitOfWork
                .Role
                .GetNextByOffsetAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<Role> AddRoleAsync
            (RoleNormalDto role,
            CancellationToken cancellationToken = default)
        {
            var result = new Role(Guid.NewGuid().ToString(),
                role.Name,
                role.Description,
                role.CreatorId);
            await _unitOfWork
                .Role
                .AddAsync(result, cancellationToken);
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
            (string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Role.GetByIdAsync(id, cancellationToken);
            _unitOfWork.Role.Delete(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
