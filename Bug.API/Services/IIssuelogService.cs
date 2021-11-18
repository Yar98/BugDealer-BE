using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface IIssuelogService
    {
        Task<IReadOnlyList<Issuelog>> GetIssuelogsByIssueIdAsync
            (string issueId,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issuelog>> GetIssuelogsByIssueIdTagIdAsync
            (string issueId,
            int tagId,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issuelog>> GetIssuelogsByIssueIdCategoryIdAsync
            (string issueId,
            int categoryId,
            CancellationToken cancellationToken = default);
    }
}
