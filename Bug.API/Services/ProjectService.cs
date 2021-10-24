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

        public async Task<IReadOnlyList<ProjectRecentDto>> GetRecentProjects(
            string accountId, int categoryId, string tagName, int count)
        {            
            count = count == 0 ? 4 : count;
            var result = await _unitOfWork
                .Project
                .GetRecentProjects(accountId, categoryId, tagName, count);
            return result
                .Where(p=>p.Tags.Count > 0)
                .Select(p => new ProjectRecentDto
                {
                    Id = p.Id,
                    Name = p.Name 
                })
                .ToList();
        }
        public async Task<ProjectNewDto> CreateProject(ProjectNewDto pro)
        {
            pro.Id = Guid.NewGuid().ToString();
            var result = _projectBuilder
                .AddId(pro.Id)
                .AddName(pro.Name)
                .AddCode(pro.Code)
                .AddStartDate(pro.StartDate)
                .AddEndDate(pro.EndDate)
                .AddCreatorId(pro.CreatorId)
                .AddWorkflowId(pro.WorkflowId)
                .Build();
            await _unitOfWork
                .Project
                .AddAsync(result);
            return pro;
        }
        public async Task<ProjectDetailDto> GetDetailProject(string id)
        {
            var result = await _unitOfWork
                .Project
                .GetDetailProject(id);
            return new ProjectDetailDto
            {
                Id = result.Id,
                Name = result.Name,
                Code = result.Code,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                ProjectType = result.ProjectType,
                Description = result.Description,
                CreatorId = result.CreatorId,
                DefaultAssigneeId = result.DefaultAssigneeId,
                WorkflowId = result.WorkflowId
            };
        }

    }
}
