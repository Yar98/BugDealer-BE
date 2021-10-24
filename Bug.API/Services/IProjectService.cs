using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.API.Services.DTO;
using Bug.Entities.Model;

namespace Bug.API.Services
{
    public interface IProjectService
    {
        Task<IReadOnlyList<ProjectRecentDto>> GetRecentProjects(
            string accountId, int categoryId, string tagName, int count);
        Task<ProjectNewDto> CreateProject(ProjectNewDto pro);
        Task<ProjectDetailDto> GetDetailProject(string id);
    }
}
