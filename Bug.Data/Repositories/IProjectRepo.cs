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
        Task DeleteProject
            (string projectId,
            CancellationToken cancellationToken = default);
    }
}
