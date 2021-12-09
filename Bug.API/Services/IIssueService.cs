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
        Task<PaginatedListDto<Issue>> GetPaginatedDetailByProjectIdAsync
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByProjectIdAsync
            (string projectId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Issue>> GetPaginatedDetailByProjectIdReporterIdAsync
            (string projectId,
            string reportId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByProjectIdReporterIdAsync
            (string projectId,
            string reporterId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Issue>> GetPaginatedDetailByProjectIdAssigneeIdAsync
            (string projectId, 
            string assigneeId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByProjectIdAssigneeIdAsync
            (string projectId, 
            string assigneeId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issue>> GetSuggestIssueByCode
            (string code,
            string accountId,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<Issue> AddIssueAsync
            (IssuePostDto issue,
            CancellationToken cancellationToken = default);
        Task UpdateIssueAsync
            (IssuePostDto issue,
            CancellationToken cancellationToken = default);
        Task UpdateTagsOfIssue
            (IssuePostDto issue,
            CancellationToken cancellationToken = default);
        Task UpdateFromRelationsOfIssue
            (IssuePostDto issue,
            CancellationToken cancellationToken = default);
        Task UpdateAttachmentsOfIssue
            (IssuePostDto issue,
            CancellationToken cancellationToken = default);
        Task DeleteIssueAsync
            (string id,
            CancellationToken cancellationToken = default);
    }
}
