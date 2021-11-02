using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bug.Entities.Model;
using Bug.Data.Infrastructure;
using Bug.API.Dto;
using Bug.API.Dto.Integration;
using Bug.Core.Common;
using Bug.Entities.Builder;
using Bug.Core.Utility;
using Bug.Data.Specifications;


namespace Bug.API.Services
{
    public class ProjectService : IProjectService, IProjectIntegrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<ProjectNormalDto> GetNormalProject(string projectId)
        {
            ProjectNormalSpecification specificationResult =
                new ProjectNormalSpecification(projectId);
            var result = await _unitOfWork
                .Project
                .GetProject(specificationResult);
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
                DefaultAssigneeName = result.DefaultAssignee?.FullName,
                WorkflowId = result.WorkflowId,
                WorkflowName = result.Workflow?.Name
            };
        }
        
        public async Task<ProjectDetailDto> GetDetailProject(string projectId)
        {
            ProjectDetailSpecification specificationResult =
                new ProjectDetailSpecification(projectId);
            var result = await _unitOfWork
                .Project
                .GetProject(specificationResult);
            return new ProjectDetailDto
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
                CreatorName = result.Creator.FullName,
                DefaultAssigneeId = result.DefaultAssigneeId,
                DefaultAssigneeName = result.DefaultAssignee?.FullName,
                WorkflowId = result.WorkflowId,
                WorkflowName = result.Workflow?.Name
            };
        }
        /*
        public async Task<IReadOnlyList<ProjectLowDto>> GetRecentProjects(
            string creatorId,
            int categoryId, 
            string tagName,
            int count)
        {
            var specificationResult =
                new ProjectWithCreatorTagsSpecification(creatorId, categoryId, tagName);
            var result = await _unitOfWork
                .Project
                .GetRecentProjects(creatorId, categoryId, tagName, count, specificationResult);
            return result
                .Where(p=>p.Tags.Count > 0)
                .Select(p => new ProjectLowDto
                {
                    Id = p.Id,
                    Name = p.Name 
                })
                .ToList();
        }
        */

        // filter by tags, return not contain tags
        public async Task<ProjectsPaginatedListDto<ProjectNormalDto>> GetPaginated
            (string accountId,
            int pageIndex,
            int pageSize,
            int categoryId,
            string tagName,
            string sortOrder,
            int accountType)
        {
            // filter project by creator
            var specificationResult =
                new ProjectByCreatorWithTagsIssuesSpecification(accountId, categoryId, tagName);
            // filter project which member working on
            var specificationResult1 =
                new ProjectsWithMemberIssuesTagsSpecification(accountId, categoryId, tagName);
            var result = await _unitOfWork
                .Project
                .GetPaginatedProjects(
                pageIndex, pageSize,
                categoryId, tagName, sortOrder,
                accountType == Bts.Creator ? specificationResult : specificationResult1);
            return new ProjectsPaginatedListDto<ProjectNormalDto>
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
                    TotalCloseIssues = p.Issues == null ? 0 : 
                    p.Issues
                    .Where(i => i.Tags.Where(
                        t => t.Name == "Close" &&
                        t.CategoryId == Bts.IssueTag).Any())
                    .Count(),
                    TotalIssues = p.Issues == null ? 0 : 
                    p.Issues.Count,
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
        public async Task<IReadOnlyList<ProjectNormalDto>> GetNextByOffset
            (string accountId,
            int offset,
            int next,
            int categoryId,
            string tagName,
            string sortOrder,
            int accountType)
        {
            // filter projects by creator
            var specificationResult =
                new ProjectByCreatorWithTagsSpecification(accountId, categoryId, tagName);
            // filter projects which member working on
            var specificationResult1 =
                new ProjectsWithMemberTagsSpecification(accountId, categoryId, tagName);
            var result = await _unitOfWork
                .Project
                .GetNextProjectsByOffset(
                offset,
                next,
                sortOrder,
                accountType == Bts.Creator ? specificationResult : specificationResult1);
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
                    TotalCloseIssues = p.Issues
                    .Where(i=>i.Tags.Where(
                        t=>t.Name=="Close"&&
                        t.CategoryId==Bts.IssueTag).Any())
                    .Count(),
                    TotalIssues = p.Issues.Count,
                    TotalOpenIssues = p.Issues
                    .Where(i => i.Tags.Where(
                        t => t.Name == "Open" &&
                        t.CategoryId == Bts.IssueTag).Any())
                    .Count()
                })
                .ToList();
        }
        
        public async Task<ProjectNormalDto> AddProject(ProjectNormalDto pro)
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
                .Build();
            await _unitOfWork
                .Project
                .AddAsync(result);
            return pro;
        }
        public async Task UpdateDetailProject(ProjectDetailDto pro)
        {
            var result = new ProjectBuilder()
                .AddId(pro.Id)
                .AddName(pro.Name)
                .AddCode(pro.Code)
                .AddStartDate(pro.StartDate)
                .AddEndDate(pro.EndDate)
                .AddRecentDate(pro.RecentDate)
                .AddDescription(pro.Description)
                .AddProjectType(pro.ProjectType)
                .AddAvatarUri(pro.AvatarUri)
                .AddWorkflowId(pro.WorkflowId)
                .AddCreatorId(pro.CreatorId)
                .AddDefaultAssigneeId(pro.DefaultAssigneeId)
                .Build();
            await _unitOfWork.Project.UpdateAsync(result);
        }
        public async Task DeleteProject(string projectId)
        {
            var result = await _unitOfWork.Project.GetByIdAsync(projectId);
            await _unitOfWork.Project.DeleteAsync(result);
        }

    }
}
