using Bug.API.Dto;
using Bug.Data.Infrastructure;
using Bug.Data.Specifications;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Comment> GetDetailCommentByIdAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new CommentSpecification(id);
            return await _unitOfWork
                .Comment
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
        }

        public async Task<IReadOnlyList<Comment>> GetCommentsByIssueIdAsync
            (string issueId,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new CommentsByIssueIdSpecification(issueId);
            return await _unitOfWork
                .Comment
                .GetAllEntitiesBySpecAsync(specificationResult, cancellationToken);
        }

        public async Task<Comment> AddCommentAsync
            (CommentNormalDto cmt,
            CancellationToken cancellationToken = default)
        {
            var result = 
                new Comment(0, cmt.Content, cmt.TimeLog, cmt.IssueId, cmt.AccountId);
            await _unitOfWork.Comment.AddAsync(result, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return result;
        }

        public async Task UpdateCommentByIdAsync
            (CommentNormalDto cmt,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Comment
                .GetByIdAsync(cmt.Id,cancellationToken);
            result.UpdateAccountId(cmt.AccountId);
            result.UpdateContent(cmt.Content);
            result.UpdateIssueId(cmt.IssueId);
            result.UpdateTimeLog(cmt.TimeLog);
            _unitOfWork.Comment.Update(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task DeleteCommentByIdAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Comment.GetByIdAsync(id, cancellationToken);
            _unitOfWork.Comment.Delete(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
