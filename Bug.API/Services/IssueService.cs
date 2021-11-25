﻿using System;
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
        public IssueService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<Issue> GetDetailIssueAsync
            (string id,
            CancellationToken cancellationToken = default)
        {
            IssueSpecification specificationResult =
                new(id);
            return await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
        }

        public async Task<PaginatedListDto<Issue>> GetPaginatedDetailByProjectIdAsync
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            IssueByProjectSpecification specificationResult =
                new(projectId);
            var result = await _unitOfWork
                .Issue
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByProjectIdAsync
            (string projectId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssueByProjectSpecification(projectId);
            var result = await _unitOfWork
                .Issue
                .GetNextByOffsetNoTrackBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<PaginatedListDto<Issue>> GetPaginatedDetailByReporterIdAsync
            (string reportId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByReporterIdSpecification(reportId);
            var result = await _unitOfWork
                .Issue
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByReporterIdAsync
            (string reporterId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByReporterIdSpecification(reporterId);
            var result = await _unitOfWork
                .Issue
                .GetNextByOffsetNoTrackBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<PaginatedListDto<Issue>> GetPaginatedDetailByAssigneeIdAsync
            (string assigneeId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByAssigneeIdSpecification(assigneeId);
            var result = await _unitOfWork
                .Issue
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByAssigneeIdAsync
            (string assigneeId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByAssigneeIdSpecification(assigneeId);
            var result = await _unitOfWork
                .Issue.GetNextByOffsetNoTrackBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<IReadOnlyList<Issue>> GetSuggestIssueByCode
            (string code,
            string projectId,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByCodeProjectIdSpecification(code, projectId);
            return await _unitOfWork
                .Issue
                .GetAllEntitiesBySpecAsync(specificationResult, cancellationToken);
        }

        public async Task<Issue> AddIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = new IssueBuilder()
                .AddId(Guid.NewGuid().ToString())
                .AddDescription(issue.Description)
                .AddAssigneeId(issue.AssigneeId)
                .AddCreatedDate(issue.CreatedDate)
                .AddDueDate(issue.DueDate)
                .AddWorklogDate(issue.WorklogDate)
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

            var customLabelTags = issue
                .Tags
                .Select(l => 
                {                     
                    var item = new Tag(l.Id, l.Name, l.Description, l.Color, l.CategoryId);
                    if (item.Id == 0)
                        _unitOfWork.Tag.Add(item);
                    else
                        _unitOfWork.Tag.Attach(item);                   
                    return item;
                })
                .ToList();
            result.UpdateTags(customLabelTags);

            var attachments = issue
                .Attachments
                .Select(a => 
                { 
                    var item = new Attachment(a.Id, a.Uri, a.IssueId);
                    if (item.Id == 0)
                        _unitOfWork.Attachment.Add(item);
                    else
                        _unitOfWork.Attachment.Attach(item);
                    return item;
                })
                .ToList();
            result.UpdateAttachments(attachments);

            var fromRelations = issue
                .FromRelations
                .Select(r => 
                { 
                    var item = new Relation(r.Id, r.Description, r.TagId, result.Id, r.ToIssueId);
                    if (item.Id == 0)
                        _unitOfWork.Relation.Add(item);
                    else
                        _unitOfWork.Relation.Attach(item);
                    return item;
                })
                .ToList();
            result.UpdateFromRelations(fromRelations);

            await _unitOfWork
                .Issue
                .AddAsync(result, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return result;
        }

        public async Task UpdateIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Issue.GetByIdAsync(issue.Id, cancellationToken);
            if(issue.AssigneeId != null)
                result.UpdateAssigneeId(issue.AssigneeId);           
            if(issue.Description != null)
                result.UpdateDescription(issue.Description);
            if(issue.Environment != null)
                result.UpdateEnvironment(issue.Environment);
            result.UpdateLogDate(issue.LogDate);
            result.UpdateCreatedDate(issue.CreatedDate);
            result.UpdateDueDate(issue.DueDate);
            result.UpdateWorklogDate(issue.WorklogDate);
            if (issue.OriginEstimateTime != null)
                result.UpdateOriginalEstimateTime(issue.OriginEstimateTime);
            if(issue.PriorityId != 0)
                result.UpdatePriorityId(issue.PriorityId);
            if(issue.ProjectId != null)
                result.UpdateProjectId(issue.ProjectId);
            if(issue.RemainEstimateTime != null)
                result.UpdateRemainEstimateTime(issue.RemainEstimateTime);
            if(issue.ReporterId != null)
                result.UpdateReporterId(issue.ReporterId);
            if(issue.StatusId != null)
                result.UpdateStatusId(issue.StatusId);
            if(issue.Title != null)
                result.UpdateTitle(issue.Title);
            _unitOfWork.Issue.Update(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateTagsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Issue.GetByIdAsync(issue.Id, cancellationToken);
            var customLabelTags = issue
                .Tags
                .Select(l => new Tag(l.Id, l.Name, l.Description, l.Color, l.CategoryId))
                .ToList();
            result.UpdateTags(customLabelTags);
            _unitOfWork.Issue.Update(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateFromRelationsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Issue.GetByIdAsync(issue.Id, cancellationToken);
            var fromRelations = issue
                .FromRelations
                .Select(r => new Relation(r.Id, r.Description, r.TagId, result.Id, r.ToIssueId))
                .ToList();
            result.UpdateFromRelations(fromRelations);
            _unitOfWork.Issue.Update(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateAttachmentsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Issue.GetByIdAsync(issue.Id, cancellationToken);
            var attachments = issue
                .Attachments
                .Select(a => new Attachment(a.Id, a.Uri, a.IssueId))
                .ToList();
            result.UpdateAttachments(attachments);
            _unitOfWork.Issue.Update(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task DeleteIssueAsync
            (string id, 
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Issue.GetByIdAsync(id, cancellationToken);
            _unitOfWork.Issue.Delete(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}