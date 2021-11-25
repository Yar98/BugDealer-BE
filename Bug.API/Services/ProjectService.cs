using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bug.Entities.Model;
using Bug.Data.Infrastructure;
using Bug.API.Dto;
using Bug.Core.Common;
using Bug.Entities.Builder;
using Bug.Core.Utils;
using Bug.Data.Specifications;
using System.Threading;

namespace Bug.API.Services
{
    public class ProjectService : IProjectService, IProjectIntegrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<ProjectNormalDto> GetNormalProjectAsync
            (string projectId,
            CancellationToken cancellationToken = default)
        {
            ProjectSpecification specificationResult =
                new(projectId);
            var result = await _unitOfWork
                .Project
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            return new ProjectNormalDto
            {
                Id = result.Id,
                Name = result.Name,
                Code = result.Code,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                RecentDate = result.RecentDate,
                Description = result.Description,
                AvatarUri = result.AvatarUri,
                CreatorId = result.CreatorId,
                DefaultAssigneeId = result.DefaultAssigneeId,
                TemplateId = result.TemplateId,
                Status = result.Status
            };
        }
        
        public async Task<Project> GetDetailProjectAsync
            (string projectId,
            CancellationToken cancellationToken = default)
        {
            ProjectSpecification specificationResult =
                new(projectId);
            var result = await _unitOfWork
                .Project
                .GetEntityBySpecAsync(specificationResult,cancellationToken);
            return result;
        }

        // filter by tags, return not contain tags
        public async Task<PaginatedListDto<Project>> GetPaginatedByCreatorIdTagIdAsync
            (string accountId,
            int pageIndex,
            int pageSize,
            int tagId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            // filter project by creator
            var specificationResult =
                new ProjectsByStatusCreatorIdTagIdSpecification(accountId, tagId);
            var result = await _unitOfWork
                .Project
                .GetPaginatedNoTrackBySpecAsync(
                pageIndex, pageSize,
                sortOrder,
                specificationResult,
                cancellationToken);
            
            return new PaginatedListDto<Project>
            {
                Items = result,
                Length = result.Length
            };
        }
        // filter by tags, return not contain tags
        public async Task<PaginatedListDto<Project>> GetPaginatedByMemberIdTagIdAsync
            (string accountId,
            int pageIndex,
            int pageSize,
            int tagId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            // filter project which member working on
            var specificationResult =
                new ProjectsByStatusWhichMemberIdJoinSpecification(accountId, tagId);
            var result = await _unitOfWork
                .Project
                .GetPaginatedNoTrackBySpecAsync(
                pageIndex, pageSize,
                sortOrder,
                specificationResult,
                cancellationToken);
            return new PaginatedListDto<Project>
            {
                Items = result,
                Length = result.Length
            };
        }
        // filter by tags, return not contain tags
        public async Task<IReadOnlyList<Project>> GetNextByOffsetByCreatorIdTagIdAsync
            (string accountId,
            int offset,
            int next,
            int tagId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            // filter projects by creator
            var specificationResult =
                new ProjectsByStatusCreatorIdTagIdSpecification(accountId, tagId);
            var result = await _unitOfWork
                .Project
                .GetNextByOffsetNoTrackBySpecAsync(
                offset,
                next,
                sortOrder,
                specificationResult,
                cancellationToken);
            return result;
        }

        // filter by tags, return not contain tags
        public async Task<IReadOnlyList<Project>> GetNextByOffsetByMemberIdTagIdAsync
            (string accountId,
            int offset,
            int next,
            int tagId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            // filter projects which member working on
            var specificationResult =
                new ProjectsByStatusWhichMemberIdJoinSpecification(accountId, tagId);
            var result = await _unitOfWork
                .Project
                .GetNextByOffsetNoTrackBySpecAsync(
                offset,
                next,
                sortOrder,
                specificationResult,
                cancellationToken);
            return result;
        }

        public async Task<Project> AddProjectAsync
            (ProjectNormalDto pro,
            CancellationToken cancellationToken = default)
        {
            pro.Id = Guid.NewGuid().ToString();
            var result = new ProjectBuilder()
                .AddId(pro.Id)
                .AddName(pro.Name)
                .AddCode(pro.Code)
                .AddStartDate(pro.StartDate)
                .AddEndDate(pro.EndDate)
                .AddRecentDate(pro.RecentDate)
                .AddDescription(pro.Description)
                .AddAvatarUri(pro.AvatarUri)
                .AddCreatorId(pro.CreatorId)
                .AddStatus(pro.Status)
                .AddTemplateId(pro.TemplateId)
                .Build();
            // add creator as member to project
            var acc = await _unitOfWork.Account.GetByIdAsync(pro.CreatorId, cancellationToken);
            //_unitOfWork.Account.Attach(acc);
            result.AddExistAccount(acc);
            // add default roles to project
            var defaultRoles = await _unitOfWork.Role.GetDefaultRolesAsync(cancellationToken:cancellationToken);
            result.AddDefaultRoles(defaultRoles);
            // add default statuses to project
            var defaultStatuses = await _unitOfWork.Status.GetDefaultStatusesAsync(cancellationToken: cancellationToken);
            result.AddDefaultStatuses(defaultStatuses);
            
            await _unitOfWork
                .Project
                .AddAsync(result, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return result;
        }

        public async Task UpdateBasicProjectAsync
            (ProjectNormalDto pro,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Project.GetByIdAsync(pro.Id,cancellationToken);
            result.UpdateName(pro.Name);
            result.UpdateAvatarUri(pro.AvatarUri);
            result.UpdateCode(pro.Code);
            result.UpdateDescription(pro.Description);
            result.UpdateEndDate(pro.EndDate);
            result.UpdateStartDate(pro.StartDate);
            result.UpdateCreatorId(pro.CreatorId);
            result.UpdateDefaultAssigneeId(pro.DefaultAssigneeId);
            result.UpdateStatus(pro.Status);
            result.UpdateTemplateId(pro.TemplateId);
            // update db
            _unitOfWork.Project.Update(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateRolesOfProjectAsync
            (ProjectNormalDto pro,
            CancellationToken cancellationToken = default)
        {
            var project = await _unitOfWork.Project.GetByIdAsync(pro.Id, cancellationToken);
            var roles = pro
                .Roles
                .Select(r => new Role(r.Id, r.Name, r.Description, r.CreatorId))
                .ToList();
            project.UpdateRoles(roles);
            _unitOfWork.Project.Update(project);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateStatusesOfProjectAsync
            (ProjectNormalDto pro,
            CancellationToken cancellationToken = default)
        {
            var project = await _unitOfWork.Project.GetByIdAsync(pro.Id, cancellationToken);
            var statuses = pro
                .Statuses
                .Select(r => new Status(r.Id, r.Name, r.Description, r.Progress, r.CreatorId, r.TagId))
                .ToList();
            project.UpdateStatuses(statuses);
            _unitOfWork.Project.Update(project);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task DeleteProjectAsync
            (string projectId,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Project.GetByIdAsync(projectId,cancellationToken);
            _unitOfWork.Project.Delete(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

    }
}
