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
                .GetEntityAsync(specificationResult, cancellationToken);
            return new ProjectNormalDto
            {
                Id = result.Id,
                Name = result.Name,
                Code = result.Code,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                RecentDate = result.RecentDate,
                ProjectType = result.ProjectType,
                Description = result.Description,
                AvatarUri = result.AvatarUri,
                CreatorId = result.CreatorId,
                CreatorName = result.Creator?.FullName,
                DefaultAssigneeId = result.DefaultAssigneeId,
                DefaultAssigneeName = result.DefaultAssignee?.FullName
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
                .GetEntityAsync(specificationResult,cancellationToken);
            return result;
        }

        // filter by tags, return not contain tags
        public async Task<PaginatedListDto<ProjectNormalDto>> GetPaginatedByCreatorAsync
            (string accountId,
            int pageIndex,
            int pageSize,
            int categoryId,
            string tagName,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            // filter project by creator
            var specificationResult =
                new ProjectsByCreatorTagWithIssuesSpecification(accountId, categoryId, tagName);
            var result = await _unitOfWork
                .Project
                .GetPaginatedAsync(
                pageIndex, pageSize,
                sortOrder,
                specificationResult,
                cancellationToken);
            
            return new PaginatedListDto<ProjectNormalDto>
            {
                Items = result
                .Where(p => p.Tags.Count > 0)
                .Select(p => new ProjectNormalDto
                {
                    Id = p.Id,
                    AvatarUri = p.AvatarUri,
                    Code = p.Code,
                    Name = p.Name,
                    EndDate = p.EndDate,
                    StartDate = p.StartDate,
                    RecentDate = p.RecentDate,
                    TotalIssues = p.Issues == null ? 0 : p.Issues.Count,
                    TotalCloseIssues = p.Issues == null ? 0 : 
                    p.Issues
                    .Where(i => i.Tags.Where(
                        t => t.Name == "Close" &&
                        t.CategoryId == Bts.IssueTag).Any())
                    .Count(),
                    TotalOpenIssues = p.Issues == null ? 0 : 
                    p.Issues
                    .Where(i => i.Tags.Where(
                        t => t.Name == "Open" &&
                        t.CategoryId == Bts.IssueTag).Any())
                    .Count()
                }).ToList(),
                Length = result.Length
            };
        }
        // filter by tags, return not contain tags
        public async Task<PaginatedListDto<ProjectNormalDto>> GetPaginatedByMemberAsync
            (string accountId,
            int pageIndex,
            int pageSize,
            int categoryId,
            string tagName,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            // filter project which member working on
            var specificationResult =
                new ProjectsWhichMemberJoinSpecification(accountId, categoryId, tagName);
            var result = await _unitOfWork
                .Project
                .GetPaginatedAsync(
                pageIndex, pageSize,
                sortOrder,
                specificationResult,
                cancellationToken);
            return new PaginatedListDto<ProjectNormalDto>
            {
                Items = result
                .Where(p => p.Tags.Count > 0)
                .Select(p => new ProjectNormalDto
                {
                    Id = p.Id,
                    AvatarUri = p.AvatarUri,
                    Code = p.Code,
                    Name = p.Name,
                    EndDate = p.EndDate,
                    StartDate = p.StartDate,
                    RecentDate = p.RecentDate,
                    TotalIssues = p.Issues == null ? 0 : p.Issues.Count,
                    TotalCloseIssues = p.Issues == null ? 0 :
                    p.Issues
                    .Where(i => i.Tags.Where(
                        t => t.Name == "Close" &&
                        t.CategoryId == Bts.IssueTag).Any())
                    .Count(),
                    TotalOpenIssues = p.Issues == null ? 0 :
                    p.Issues
                    .Where(i => i.Tags.Where(
                        t => t.Name == "Open" &&
                        t.CategoryId == Bts.IssueTag).Any())
                    .Count()
                }).ToList(),
                Length = result.Length
            };
        }
        // filter by tags, return not contain tags
        public async Task<IReadOnlyList<ProjectNormalDto>> GetNextByOffsetByCreatorAsync
            (string accountId,
            int offset,
            int next,
            int categoryId,
            string tagName,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            // filter projects by creator
            var specificationResult =
                new ProjectsByCreatorTagWithIssuesSpecification(accountId, categoryId, tagName);
            var result = await _unitOfWork
                .Project
                .GetNextByOffsetAsync(
                offset,
                next,
                sortOrder,
                specificationResult,
                cancellationToken);
            return result
                .Where(p => p.Tags.Count > 0)
                .Select(p => new ProjectNormalDto
                {
                    Id = p.Id,
                    AvatarUri = p.AvatarUri,
                    Code = p.Code,
                    EndDate = p.EndDate,
                    Name = p.Name,
                    RecentDate = p.RecentDate,
                    StartDate = p.StartDate,
                    TotalIssues = p.Issues == null ? 0 : p.Issues.Count,
                    TotalCloseIssues = p.Issues == null ? 0 : 
                    p.Issues
                    .Where(i=>i.Tags.Where(
                        t=>t.Name=="Close"&&
                        t.CategoryId==Bts.IssueTag).Any())
                    .Count(),
                    TotalOpenIssues = p.Issues == null ? 0 : 
                    p.Issues
                    .Where(i => i.Tags.Where(
                        t => t.Name == "Open" &&
                        t.CategoryId == Bts.IssueTag).Any())
                    .Count()
                })
                .ToList();
        }

        // filter by tags, return not contain tags
        public async Task<IReadOnlyList<ProjectNormalDto>> GetNextByOffsetByMemberAsync
            (string accountId,
            int offset,
            int next,
            int categoryId,
            string tagName,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            // filter projects which member working on
            var specificationResult =
                new ProjectsWhichMemberJoinSpecification(accountId, categoryId, tagName);
            var result = await _unitOfWork
                .Project
                .GetNextByOffsetAsync(
                offset,
                next,
                sortOrder,
                specificationResult,
                cancellationToken);
            return result
                .Where(p => p.Tags.Count > 0)
                .Select(p => new ProjectNormalDto
                {
                    Id = p.Id,
                    AvatarUri = p.AvatarUri,
                    Code = p.Code,
                    EndDate = p.EndDate,
                    Name = p.Name,
                    RecentDate = p.RecentDate,
                    StartDate = p.StartDate,
                    TotalIssues = p.Issues == null ? 0 : p.Issues.Count,
                    TotalCloseIssues = p.Issues == null ? 0 :
                    p.Issues
                    .Where(i => i.Tags.Where(
                        t => t.Name == "Close" &&
                        t.CategoryId == Bts.IssueTag).Any())
                    .Count(),
                    TotalOpenIssues = p.Issues == null ? 0 :
                    p.Issues
                    .Where(i => i.Tags.Where(
                        t => t.Name == "Open" &&
                        t.CategoryId == Bts.IssueTag).Any())
                    .Count()
                })
                .ToList();
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
                .AddAvatarUri(pro.AvatarUri)
                .AddStartDate(pro.StartDate)
                .AddEndDate(pro.EndDate)
                .AddRecentDate(pro.RecentDate)
                .AddCreatorId(pro.CreatorId)
                .Build();
            var acc = await _unitOfWork.Account.GetByIdAsync(pro.CreatorId, cancellationToken);
            result.AddAccount(acc);
            await _unitOfWork
                .Project
                .AddAsync(result, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return result;
        }

        public async Task UpdateProjectAsync
            (ProjectNormalDto pro,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Project.GetByIdAsync(pro.Id,cancellationToken);
            result.UpdateName(pro.Name);
            result.UpdateAvatarUri(pro.AvatarUri);
            result.UpdateCode(pro.Code);
            result.UpdateCreatorId(pro.CreatorId);
            result.UpdateDefaultAssigneeId(pro.DefaultAssigneeId);
            result.UpdateDescription(pro.Description);
            result.UpdateEndDate(pro.EndDate);
            result.UpdateProjectType(pro.ProjectType);
            result.UpdateStartDate(pro.StartDate);
            // add default roles to current project
            var defaultRoles = await _unitOfWork.Role.GetDefaultRoles(cancellationToken: cancellationToken);
            result.AddDefaultRoles(defaultRoles);
            // add default statuses to current project
            var defaultStatuses = await _unitOfWork.Status.GetDefaultStatuses(cancellationToken: cancellationToken);
            result.AddDefaultStatuses(defaultStatuses);
            // update db
            _unitOfWork.Project.Update(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task AddExistRoleToProject
            (RoleNormalDto role,
            CancellationToken cancellationToken = default)
        {
            var p = await _unitOfWork.Project.GetByIdAsync(role.ProjectId, cancellationToken);
            var r = await _unitOfWork.Role.GetByIdAsync(role.Id, cancellationToken);
            p.AddExistRole(r);
            _unitOfWork.Project.Update(p);
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
