using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Bug.Data.Infrastructure;
using Bug.API.Services.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using Bug.Core.Common;
using Bug.Entities.Builder;
using Bug.Core.Utility;

namespace Bug.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IProjectBuilder _projectBuilder;
        public ProjectService(IUnitOfWork uow, IProjectBuilder pd)
        {
            _unitOfWork = uow;
            _projectBuilder = pd;
        }

        public async Task<ProjectNormalDto> GetNormalProject(string projectId)
        {
            var result = await _unitOfWork
                .Project
                .GetByIdAsync(projectId);
            return new ProjectNormalDto
            {
                Id = result.Id,
                Name = result.Name,
                Code = result.Code,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                RecentDate = result.RecentDate,
                AvatarUri = result.AvatarUri,
                CreatorId = result.CreatorId,
                WorkflowId = result.WorkflowId
            };
        }
        public async Task<ProjectDetailDto> GetDetailProject(string projectId)
        {
            var result = await _unitOfWork
                .Project
                .GetByIdAsync(projectId);
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
                DefaultAssigneeId = result.DefaultAssigneeId,
                WorkflowId = result.WorkflowId
            };
        }
        public async Task<IReadOnlyList<ProjectLowDto>> GetRecentProjects(
            string creatorId,
            int categoryId, string tagName,
            int count)
        {
            var result = await _unitOfWork
                .Project
                .GetRecentProjects(creatorId, categoryId, tagName, count);
            return result
                .Where(p=>p.Tags.Count > 0)
                .Select(p => new ProjectLowDto
                {
                    Id = p.Id,
                    Name = p.Name 
                })
                .ToList();
        }
        public async Task<ProjectsPaginatedListDto<ProjectNormalDto>> GetPaginatedProjects(
            string creatorId,
            int pageIndex, int pageSize,
            int categoryId, string tagName,
            string sortOrder)
        {
            var result = await _unitOfWork
                .Project
                .GetPaginatedProjects(creatorId, pageIndex, pageSize, categoryId, tagName, sortOrder);

            return new ProjectsPaginatedListDto<ProjectNormalDto>
            {
                HasNextPage = result.HasNextPage,
                HasPreviousPage = result.HasPreviousPage,
                items = new PaginatedList<ProjectNormalDto>(
                    result.Where(p => p.Tags.Count > 0)
                    .Select(p => new ProjectNormalDto
                    {
                        Id = p.Id,
                        AvatarUri = p.AvatarUri,
                        Code = p.Code,
                        CreatorId = p.CreatorId,
                        EndDate = p.EndDate,
                        Name = p.Name,
                        RecentDate = p.RecentDate,
                        StartDate = p.StartDate,
                        WorkflowId = p.WorkflowId
                    }).ToList(),
                    result.TotalPages,pageIndex,pageSize),
                PageIndex = result.PageIndex,
                TotalPages = result.TotalPages
            };
        }
        public async Task<ProjectNormalDto> CreateProject(ProjectNormalDto pro)
        {
            pro.Id = Guid.NewGuid().ToString();
            var result = _projectBuilder
                .AddId(pro.Id)
                .AddName(pro.Name)
                .AddCode(pro.Code)
                .AddAvatarUri(pro.AvatarUri)
                .AddStartDate(pro.StartDate)
                .AddEndDate(pro.EndDate)
                .AddRecentDate(pro.RecentDate)
                .AddCreatorId(pro.CreatorId)
                .AddWorkflowId(pro.WorkflowId)
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
