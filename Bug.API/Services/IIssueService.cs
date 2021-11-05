using Bug.API.Dto;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface IIssueService
    {
        Task<Issue> GetIssueDetailAsync
            (string id,
            CancellationToken cancelltionToken = default);
        Task<PaginatedListDto<Issue>> GetPaginatedByProject
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issue>> GetByOffsetByProject
            (string projectId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToke = default);
        Task<Issue> AddIssue
            (IssueDto issue,
            CancellationToken cancellationToken = default);
        Task UpdateIssue
            (IssueDto issue,
            CancellationToken cancellationToken = default);
        Task DeleteIssueAsync
            (string id,
            CancellationToken cancellationToken = default);
    }
}
