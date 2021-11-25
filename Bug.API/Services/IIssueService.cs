﻿using Bug.API.Dto;
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
        Task<PaginatedListDto<Issue>> GetPaginatedDetailByReporterIdAsync
            (string reportId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByReporterIdAsync
            (string reporterId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<Issue>> GetPaginatedDetailByAssigneeIdAsync
            (string assigneeId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByAssigneeIdAsync
            (string assigneeId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issue>> GetSuggestIssueByCode
            (string code,
            string projectId,
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