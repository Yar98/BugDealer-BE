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
using System.Text.RegularExpressions;

namespace Bug.API.Services
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IssueService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<Issue> GetNormalIssueAsync
            (string id,
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork
                .Issue
                .GetByIdAsync(id, cancellationToken);
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

        public async Task<PaginatedListDto<Issue>> GetPaginatedByRelateUserAsync
            (string accountId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByRelateUserSpecification(accountId);
            var result = await _unitOfWork
                .Issue
                .GetPaginatedBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Issue>> GetNextByOffsetByRelateUserAsync
            (string accountId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByRelateUserSpecification(accountId);
            var result = await _unitOfWork
                .Issue
                .GetNextByOffsetBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
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
                .GetPaginatedBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
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
                .GetNextByOffsetBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<PaginatedListDto<Issue>> GetPaginatedDetailByProjectIdReporterIdAsync
            (string projectId,
            string reportId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByReporterIdProjectIdSpecification(projectId, reportId);
            var result = await _unitOfWork
                .Issue
                .GetPaginatedBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByProjectIdReporterIdAsync
            (string projectId,
            string reporterId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByReporterIdProjectIdSpecification(projectId, reporterId);
            var result = await _unitOfWork
                .Issue
                .GetNextByOffsetBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<PaginatedListDto<Issue>> GetPaginatedDetailByProjectIdAssigneeIdAsync
            (string projectId,
            string assigneeId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByAssigneeIdProjectIdSpecification(projectId,assigneeId);
            var result = await _unitOfWork
                .Issue
                .GetPaginatedBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByProjectIdAssigneeIdAsync
            (string projectId,
            string assigneeId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByAssigneeIdSpecification(assigneeId);
            var result = await _unitOfWork
                .Issue
                .GetNextByOffsetBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<IReadOnlyList<Issue>> GetSuggestIssueByCode
            (string code,
            string accountId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var regexNumber = new Regex(@"[0-9]+$");
            var regexChar = new Regex(@"^[A-Z]+");
            var specificationResult =
                new IssuesByProjectCodeSpecification(int.Parse(regexNumber.Match(code).Value), regexChar.Match(code).Value, accountId);
            return await _unitOfWork
                .Issue
                .GetAllEntitiesBySpecAsync(specificationResult, sortOrder, cancellationToken);
        }

        public async Task<Issue> AddIssueAsync
            (IssuePostDto issue,
            CancellationToken cancellationToken = default)
        {
            var pro = await _unitOfWork
                .Project
                .GetByIdAsync(issue.ProjectId, cancellationToken);
            var result = new IssueBuilder()
                .AddId(Guid.NewGuid().ToString())
                .AddDescription(issue.Description)
                .AddNumberCode(pro.Temp)
                .AddAssigneeId(issue.AssigneeId)
                .AddCreatedDate(issue.CreatedDate)
                .AddDueDate(issue.DueDate)
                .AddWorklogDate(issue.WorklogDate)
                .AddEnvironment(issue.Environment)
                .AddOriginEstimateTime(issue.OriginEstimateTime)
                .AddPriorityId(issue.PriorityId)
                .AddSeverityId(issue.SeverityId)
                .AddProjectId(issue.ProjectId)
                .AddRemainEstimateTime(issue.RemainEstimateTime)
                .AddReporterId(issue.ReporterId)
                .AddStatusId(issue.StatusId)
                .AddLogDate(issue.LogDate)
                .AddTitle(issue.Title)
                .Build();
            pro.Temp += 1;
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
                    var item = new Relation(r.Description, r.TagId, result.Id, r.ToIssueId);
                    return item;
                })
                .ToList();
            result.UpdateFromRelations(fromRelations);

            await _unitOfWork
                .Issue
                .AddAsync(result, cancellationToken);

            _unitOfWork.Save();

            return result;
        }

        public async Task UpdateIssueAsync
            (IssuePostDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Issue
                .GetByIdAsync(issue.Id, cancellationToken);
            if(issue.AssigneeId != null)
                result.UpdateAssigneeId(issue.AssigneeId);           
            if(issue.Description != null)
                result.UpdateDescription(issue.Description);
            if(issue.Environment != null)
                result.UpdateEnvironment(issue.Environment);
            if(issue.LogDate != null)
                result.UpdateLogDate(issue.LogDate);
            if(issue.CreatedDate != null)
                result.UpdateCreatedDate(issue.CreatedDate);
            if(issue.DueDate != null)
                result.UpdateDueDate(issue.DueDate);
            if(issue.WorklogDate != null)
                result.UpdateWorklogDate(issue.WorklogDate);
            if (issue.OriginEstimateTime != null)
                result.UpdateOriginalEstimateTime(issue.OriginEstimateTime);
            if(issue.PriorityId != 0 && issue.PriorityId != null)
                result.UpdatePriorityId(issue.PriorityId);
            if (issue.SeverityId != 0 && issue.SeverityId != null)
                result.UpdateSeverityId(issue.SeverityId);
            if (issue.RemainEstimateTime != null)
                result.UpdateRemainEstimateTime(issue.RemainEstimateTime);
            if(issue.ReporterId != null)
                result.UpdateReporterId(issue.ReporterId);
            if(issue.StatusId != null)
                result.UpdateStatusId(issue.StatusId);
            if(issue.Title != null)
                result.UpdateTitle(issue.Title);
            
            _unitOfWork.Save();
        }       

        public async Task UpdateTagsOfIssue
            (IssuePostDto issue,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssueSpecification(issue.Id);
            var result = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(specificationResult,cancellationToken);
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
            
            _unitOfWork.Save();
        }

        public async Task UpdateFromRelationsOfIssue
            (IssuePostDto issue,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssueSpecification(issue.Id);
            var result = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            var fromRelations = issue
                .FromRelations
                .Select(r => new Relation(r.Description, r.TagId, result.Id, r.ToIssueId))
                .ToList();
            result.UpdateFromRelations(fromRelations);
            
            _unitOfWork.Save();
        }

        public async Task UpdateAttachmentsOfIssue
            (IssuePostDto issue,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssueSpecification(issue.Id);
            var result = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            var delAttach = result
                .Attachments
                .Where(at => !issue.Attachments.Any(a => a.Id == at.Id))
                .ToList();
            if (delAttach != null)
            {
                Parallel.ForEach(delAttach, d =>
                {
                    _unitOfWork.Attachment.Delete(d);
                });
            }
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
            
            _unitOfWork.Save();
        }

        public async Task DeleteIssueAsync
            (string id, 
            CancellationToken cancellationToken = default)
        {
            await _unitOfWork
                .Relation
                .DeleteRelationByIssueAsync(id);
            var result = _unitOfWork
                .Issue
                .GetByIdAsync(id, cancellationToken).Result;
            _unitOfWork.Issue.Delete(result);

            _unitOfWork.Save();
        }
    }
}
