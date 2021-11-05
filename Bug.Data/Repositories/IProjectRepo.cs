using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Core.Utility;
using Bug.Data.Specifications;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public interface IProjectRepo : IEntityRepoBase<Project>
    {
        /*
        Task<IReadOnlyList<Project>> GetRecentProjects(
            string accountId,
            int categoryId, string tagName,
            int count, 
            ISpecification<Project> specificationResult, 
            CancellationToken cancelltionToken = default);
        */
        Task<Project> GetProjectAsync
            (ISpecification<Project> specificationResult,
            CancellationToken cancelltionToken = default);
        Task<PaginatedList<Project>> GetPaginatedProjectsAsync
            (int pageIndex, 
            int pageSize,
            string sortOrder,
            ISpecification<Project> specificationResult,
            CancellationToken cancelltionToken = default);
        Task<IReadOnlyList<Project>> GetNextProjectsByOffsetAsync
            (int offset, 
            int next,
            string sortOrder,
            ISpecification<Project> specificationResult,
            CancellationToken cancelltionToken = default);
        Task Test();
    }
}
