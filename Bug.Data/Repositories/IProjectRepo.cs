using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public interface IProjectRepo : IEntityRepoBase<Project>
    {
        Task<IReadOnlyList<Project>> GetRecentProjects(
            string accountId, int categoryId, string tagName, int count, CancellationToken cancelltionToken = default);
        Task<Project> GetDetailProject(string projectId);



        Task Test();
    }
}
