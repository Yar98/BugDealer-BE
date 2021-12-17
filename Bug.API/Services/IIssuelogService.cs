using Bug.API.Dto;
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
        Task<Issuelog> GetDetailIssuelogByIdAsync
            (int id,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issuelog>> GetIssuelogsByIssueIdAsync
            (string issueId,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issuelog>> GetIssuelogsByIssueIdTagIdAsync
            (string issueId,
            int tagId,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Issuelog>> GetIssuelogsByIssueIdCategoryIdAsync
            (string issueId,
            int categoryId,
            string sortOrder,
            CancellationToken cancellationToken = default);
    }
}
