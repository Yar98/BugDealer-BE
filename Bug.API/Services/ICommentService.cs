using Bug.API.Dto;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface ICommentService
    {
        Task<Comment> GetDetailCommentByIdAsync
            (int id,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Comment>> GetCommentsByIssueIdAsync
            (string issueId,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<Comment> AddCommentAsync
            (CommentNormalDto cmt,
            CancellationToken cancellationToken = default);
        Task UpdateCommentByIdAsync
            (CommentNormalDto cmt,
            CancellationToken cancellationToken = default);
        Task DeleteCommentByIdAsync
            (int id,
            CancellationToken cancellationToken = default);
    }
}
