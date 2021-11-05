using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Data.Infrastructure;
using Bug.Entities.Builder;
using Bug.Entities.Model;
using Bug.Data.Specifications;
using Bug.API.Dto;

namespace Bug.API.Services
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IIssueBuilder _issueBuilder;
        public IssueService(IUnitOfWork uow, IIssueBuilder pd)
        {
            _unitOfWork = uow;
            _issueBuilder = pd;
        }

        public async Task<Issue> GetIssueDetailAsync
            (string id,
            CancellationToken cancellationToken = default)
        {
            IssueDetailLv1Specification specificationResult =
                new IssueDetailLv1Specification(id);
            return await _unitOfWork
                .Issue
                .GetIssuelAsync(specificationResult, cancellationToken);
        }
        public async Task<PaginatedListDto<Issue>> GetPaginatedByProject
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            IssueDetailLv1ByProjectSpecification specificationResult =
                new IssueDetailLv1ByProjectSpecification(projectId);
            var result = await _unitOfWork
                .Issue
                .GetPaginatedIssuesAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
        }
        public async Task<IReadOnlyList<Issue>> GetByOffsetByProject
            (string projectId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToke = default)
        {
            IssueDetailLv1ByProjectSpecification specificationResult =
                new IssueDetailLv1ByProjectSpecification(projectId);
            var result = await _unitOfWork
                .Issue
                .GetByOffsetIssuesAsync(offset, next, sortOrder, specificationResult, cancellationToke);
            return result;
        }
        public async Task<Issue> AddIssue
            (IssueDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = new IssueBuilder()
                .AddId(Guid.NewGuid().ToString())
                .AddDescription(issue.Description)
                .AddAssigneeId(issue.AssigneeId)
                .AddCreatedDate(issue.CreatedDate)
                .AddDueDate(issue.DueDate)
                .AddEnvironment(issue.Environment)
                .AddOriginEstimateTime(issue.OriginEstimateTime)
                .AddPriorityId(issue.PriorityId)
                .AddProjectId(issue.ProjectId)
                .AddRemainEstimateTime(issue.RemainEstimateTime)
                .AddReporterId(issue.ReporterId)
                .AddStatusId(issue.StatusId)
                .AddLogDate(issue.LogDate)
                .AddTitle(issue.Title)
                .Build();
            result.UpdateTags(issue.Tags);
            return await _unitOfWork
                .Issue
                .AddAsync(result, cancellationToken);
        }
        public async Task UpdateIssue
            (IssueDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Issue.GetByIdAsync(issue.Id, cancellationToken);
            ////////////////??????????????
            await _unitOfWork.Issue.UpdateAsync(result, cancellationToken);
        }
        public async Task DeleteIssueAsync
            (string id, 
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Issue.GetByIdAsync(id, cancellationToken);
            await _unitOfWork.Issue.DeleteAsync(result, cancellationToken);
        }
    }
}
