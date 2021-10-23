using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Bug.Data.Infrastructure;
using Bug.API.Services.DTO;
using Microsoft.EntityFrameworkCore;

namespace Bug.API.Services
{
    public class ProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<IReadOnlyList<ProjectRecentDto>> GetRecentProjects(
            string accountId, int categoryId, string tagName, int count)
        {
            return await _unitOfWork
                .Project
                .GetRecentProject(accountId, categoryId, tagName, count)
                .Select(p=>new ProjectRecentDto() 
                { 
                    Id = p.Id, 
                    Name = p.Name
                })
                .ToListAsync();
        }
    }
}
