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
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<Project> GetNormalProjectAsync
            (string projectId,
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork
                .Project
                .GetByIdAsync(projectId, cancellationToken);
        }

        public async Task<Project> GetProjectsByCodeCreatorId
            (string creatorId,
            string code,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new ProjectsByCodeCreatorIdSpecification(creatorId, code);
            var result = await _unitOfWork
                .Project
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            return result;
        }
        
        public async Task<Project> GetDetailProjectAsync
            (string projectId,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new ProjectSpecification(projectId);
            var result = await _unitOfWork
                .Project
                .GetEntityBySpecAsync(specificationResult,cancellationToken);
            return result;
        }

        public async Task<PaginatedListDto<Project>> GetPaginatedByMemberIdSearchAsync
            (string accountId,
            int state,
            string search,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new ProjectsBySearchWhichMemberIdJoinSpecification(accountId, state, search);
            var result = await _unitOfWork
                .Project
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Project>
            {
                Length = result.Length,
                Items = result
            };
        }

        // filter by tags, return not contain tags
        public async Task<PaginatedListDto<Project>> GetPaginatedByCreatorIdStatusAsync
            (string accountId,
            int pageIndex,
            int pageSize,
            int tagId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            // filter project by creator
            var specificationResult =
                new ProjectsByStateCreatorIdSpecification(accountId, tagId);
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
                new ProjectsByStateCreatorIdSpecification(accountId, tagId);
            var result = await _unitOfWork
                .Project
                .GetNextByOffsetNoTrackBySpecAsync
                (offset,
                next,
                sortOrder,
                specificationResult,
                cancellationToken);
            return result;
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
                new ProjectsByStateWhichMemberIdJoinSpecification(accountId, tagId);
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
                new ProjectsByStateWhichMemberIdJoinSpecification(accountId, tagId);
            var result = await _unitOfWork
                .Project
                .GetNextByOffsetNoTrackBySpecAsync
                (offset,
                next,
                sortOrder,
                specificationResult,
                cancellationToken);
            return result;
        }

        public async Task<Project> AddProjectAsync
            (ProjectPostDto pro,
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
                .AddState()
                .AddTemplateId(pro.TemplateId??1)
                .AddDefaultRoleId(1)
                .AddDefaultStatusId("defaultStatus1")
                .Build();
            // add creator as member to project
            result
                .AccountProjectRoles
                .Add(new AccountProjectRole(pro.CreatorId, result.Id, 1));
            // add default roles to project
            var defaultRoles = await _unitOfWork
                .Role
                .GetDefaultRolesAsync(cancellationToken:cancellationToken);
            result.AddDefaultRoles(defaultRoles);
            // add default statuses to project
            var defaultStatuses = await _unitOfWork
                .Status
                .GetDefaultStatusesAsync(cancellationToken: cancellationToken);
            result.AddDefaultStatuses(defaultStatuses);
            
            await _unitOfWork
                .Project
                .AddAsync(result, cancellationToken);

            _unitOfWork.Save();
            return result;
        }

        public async Task UpdateBasicProjectAsync
            (ProjectPutDto pro,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Project
                .GetByIdAsync(pro.Id,cancellationToken);
            if (result == null)
                return;
            if(pro.Name != null)
                result.UpdateName(pro.Name);
            if(pro.AvatarUri != null)
                result.UpdateAvatarUri(pro.AvatarUri);
            if(pro.Code != null)
                result.UpdateCode(pro.Code);
            if(pro.Description != null)
                result.UpdateDescription(pro.Description);
            if(pro.EndDate != null)
                result.UpdateEndDate(pro.EndDate);
            if(pro.StartDate != null)
                result.UpdateStartDate(pro.StartDate);
            if(pro.CreatorId != null)
                result.UpdateCreatorId(pro.CreatorId);
            if(pro.DefaultAssigneeId != null)
                result.UpdateDefaultAssigneeId(pro.DefaultAssigneeId);
            if(pro.State != null)
                result.UpdateState(pro.State??0);
            if(pro.TemplateId != null)
                result.UpdateTemplateId(pro.TemplateId??0);

            // update db
            _unitOfWork.Save();
        }

        public async Task UpdateRolesOfProjectAsync
            (ProjectPutDto pro,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new ProjectSpecification(pro.Id);
            var project = await _unitOfWork
                .Project
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            var roles = await _unitOfWork
                .Role
                .GetRolesFromMutiIdsAsync(pro.Roles?.Select(r=>r.Id).Where(r=>r!=0).ToList(), cancellationToken);
            var defaultRoles = await _unitOfWork
                .Role
                .GetDefaultRolesAsync(cancellationToken:cancellationToken);           
            roles?.AddRange(defaultRoles);

            project?.UpdateDefaultRoleId(pro.DefaultRole.Id);
            project?.UpdateRoles(roles);
            _unitOfWork.Project.Update(project);
            _unitOfWork.Save();

            _unitOfWork.AccountProjectRole.UpdateMultiByRoleIdProjectId(pro.Id, roles);
        }

        public async Task UpdateStatusesOfProjectAsync
            (ProjectPutStatusesDto pro,
            CancellationToken cancellationToken = default)
        {
            var project = await _unitOfWork
                .Project
                .GetByIdAsync(pro.Id, cancellationToken);
            var statuses = await _unitOfWork
                .Status
                .GetStatusesFromMutiIdsAsync(pro.Statuses.Select(st=>st.Id).ToList(),cancellationToken);
            
            _unitOfWork.Issue.UpdateIssuesHaveDumbStatus(statuses);
            
            var defaultStatuses = await _unitOfWork
                .Status
                .GetDefaultStatusesAsync(cancellationToken: cancellationToken);
            statuses?.AddRange(defaultStatuses);

            project?.UpdateDefaultStatusId(pro.DefaultStatusId);
            project?.UpdateStatuses(statuses);

            Parallel.ForEach(pro.OldStatuses, st =>
            {
                var i = _unitOfWork.Issue.GetByIdAsync(st.FromId).Result;
                i.UpdateStatusId(st.ToId);
            });

            _unitOfWork.Save(); 
        }

        public async Task AddMemberToProjectAsync
            (string memberId,
            string projectId,
            CancellationToken cancellationToken = default)
        {
            var apr = new AccountProjectRole(memberId, projectId, 1);
            await _unitOfWork
                .AccountProjectRole
                .AddAsync(apr,cancellationToken);
            _unitOfWork.Save();
        }

        public async Task AddRoleToProjectAsync
             (string projectId,
             int roleId,
             CancellationToken cancellationToken = default)
        {
            var project = await _unitOfWork
                .Project
                .GetByIdAsync(projectId, cancellationToken);
            var role = await _unitOfWork
                .Role
                .GetByIdAsync(roleId, cancellationToken);

            project.AddExistRole(role);
            _unitOfWork.Project.Update(project);
            _unitOfWork.Save();
        }

        public async Task DeleteMemberFromProjectAsync
            (string projectId,
            string accountId,
            CancellationToken cancellationToken = default)
        {
            var project = await _unitOfWork
                .Project
                .GetByIdAsync(projectId, cancellationToken);
            if (project.CreatorId == projectId)
                return;
            await _unitOfWork
                .AccountProjectRole
                .DeleteMemberFromProjectAsync(projectId, accountId, cancellationToken);
        }

        public async Task DeleteProjectAsync
            (string projectId,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Project.GetByIdAsync(projectId,cancellationToken);
            _unitOfWork.Project.Delete(result);
            _unitOfWork.Save();
        }

    }
}
