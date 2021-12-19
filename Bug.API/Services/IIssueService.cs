using Bug.API.Dto;
using Bug.Entities.Integration;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface IIssueService
    {
        Task<Stream> ExportIssueExcelFile
            (string issueId,
            Stream stream = null,
            CancellationToken cancellationToken = default);
        Task<Issue> GetNormalIssueAsync
            (string id,
            CancellationToken cancellationToken = default);
        Task<Issue> GetDetailIssueAsync
            (string id,
            CancellationToken cancelltionToken = default);
        Task<PaginatedListDto<Issue>> GetPaginatedByFilter
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            string search,
            string statuses,
            string assignees,
            string reporters,
            string priorities,
            string severity,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Issue>> GetPaginatedByProjectIdSearchAsync
            (string search,
            string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Issue>> GetPaginatedByRelateUserAsync
            (string accountId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issue>> GetNextByOffsetByRelateUserAsync
            (string accountId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
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
            string issueId,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<RelatedIssues>> GetRelationOfIssueAsync
            (string issueId,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<Issue> AddIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default);
        Task UpdateIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default);
        Task UpdateTagsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default);
        Task UpdateAttachmentsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default);
        Task AddRelationOfIssue
            (RelationNormalDto relation,
            CancellationToken cancellationToken = default);
        Task DeleteRelationOfIssue
            (RelationNormalDto relation,
            CancellationToken cancellationToken = default);
        Task DeleteIssueAsync
            (string id,
            CancellationToken cancellationToken = default);
    }
}
