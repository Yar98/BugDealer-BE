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
