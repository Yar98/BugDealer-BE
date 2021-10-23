using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.API.Services.DTO;

namespace Bug.API.Services
{
    public interface IProjectService
    {
        Task<IReadOnlyList<ProjectRecentDto>> GetRecentProjects(
            string accountId, int categoryId, string tagName, int count);
    }
}
