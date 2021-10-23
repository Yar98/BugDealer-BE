using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public interface IProjectRepo : IEntityRepoBase<Project>
    {
        IQueryable<Project> GetRecentProject(
            string accountId, int categoryId, string tagName, int count);
        Task Test();
    }
}
