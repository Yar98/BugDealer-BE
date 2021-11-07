using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Core.Utils;
using Bug.Data.Specifications;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public interface IProjectRepo : IEntityRepoBase<Project>
    {
        Task<Project> GetProjectAsync
            (ISpecification<Project> specificationResult,
            CancellationToken cancelltionToken = default);
        
        Task Test();
    }
}
