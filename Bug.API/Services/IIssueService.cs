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
        Task<Issue> GetDetailIssueAsync
            (string id,
            CancellationToken cancelltionToken = default);
        Task<PaginatedListDto<Issue>> GetPaginatedDetailByProjectAsync
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByProjectAsync
            (string projectId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToke = default);
        Task<Issue> AddIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default);
        Task UpdateIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default);
        Task UpdateTagsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default);
        Task UpdateFromRelationsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default);
        Task UpdateAttachmentsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default);
        Task DeleteIssueAsync
            (string id,
            CancellationToken cancellationToken = default);
    }
}
