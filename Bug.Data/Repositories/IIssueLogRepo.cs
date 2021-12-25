using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public interface IIssuelogRepo : IEntityRepoBase<Issuelog>
    {
        Task<IReadOnlyList<Issuelog>> GetRecentAsync
            (string accountId,
            int offset,
            int next,
            CancellationToken cancellationToken = default);
        Task DeleteLogBeforeDelIssue
            (string issueId,
            CancellationToken cancellationToken = default);
    }
}
