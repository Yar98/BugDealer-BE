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
    public interface IIssueRepo : IEntityRepoBase<Issue>
    {
        Task DeleteIssueById(string id, CancellationToken cancellationToken = default);
        Task<PaginatedList<Issue>> GetPaginatedByFilter
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            string search,
            string statuses,
            string assignees,
            string reporters,
            string priorities,
            string severities,
            CancellationToken cancellationToken = default);
        Task UpdateTagsOfIssueAsync
            (string issueId,
            List<Tag> tags,
            CancellationToken cancellationToken = default);
        Task UpdateAttachmentsOfIssueAsync
            (string issueId,
            List<Attachment> attachments,
            CancellationToken cancellationToken = default);
        void UpdateIssuesHaveDumbStatus(List<Status> statuses);
        Task<List<Issue>> GetSuggestIssuesAsync
            (string projectId,
            string search,
            string sortOrder,
            CancellationToken cancellationToken = default);
    }
}
