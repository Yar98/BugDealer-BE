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
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using Bug.API.Utils;
using Bug.Entities.Integration;

namespace Bug.API.Services
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public IssueService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<Stream> ExportIssueExcelFile
            (string issueId, 
            Stream stream = null,
            CancellationToken cancellationToken = default)
        {
            var issue = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(new IssueSpecification(issueId), cancellationToken);
            return await new ExcelUtils()
                .CreateExcelFile(issue, stream);
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
            var specificationResult =
                new IssueSpecification(id);
            var result = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            var worklogs = await _unitOfWork
                .Worklog
                .GetAllEntitiesNoTrackBySpecAsync(new WorklogsByIssueIdSpecification(id), null, cancellationToken);
            if (worklogs != null && worklogs.Count > 0)
                result.TotalSpentTime = worklogs
                    .Select(w => w.SpentTime)
                    .ToList()
                    .Aggregate((x, y) => x + y);
            return result;
        }

        public async Task<PaginatedListDto<Issue>> GetPaginatedByFilter
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
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Issue
                .GetPaginatedByFilter(projectId, pageIndex, pageSize, sortOrder, search, statuses, assignees, reporters, priorities, severity, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<PaginatedListDto<Issue>> GetPaginatedByProjectIdSearchAsync
            (string search,
            string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByProjectIdSearchSpecification(projectId, search);
            var result = await _unitOfWork
                .Issue
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
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
            (string search,
            string projectId,
            string issueId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var result =  await _unitOfWork
                .Issue
                .GetSuggestIssuesAsync(projectId, search, sortOrder, cancellationToken);
            result.Remove(result.FirstOrDefault(i => i.Id == issueId));
            return result;
        }

        public async Task<IReadOnlyList<RelatedIssues>> GetRelationOfIssueAsync
            (string issueId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RelationByFromIssueSpecification(issueId);
            var result = await _unitOfWork
                .Relation
                .GetAllEntitiesNoTrackBySpecAsync(specificationResult, null, cancellationToken);
            return result
                .GroupBy(r => r.TagId)
                .Select(gr => new RelatedIssues
                {
                    Tag = gr.FirstOrDefault().Tag,
                    Issues = gr.Select(item => item.ToIssue).ToList()
                })
                .ToList();
        }

        public async Task<Issue> AddIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var pro = await _unitOfWork
                .Project
                .GetByIdAsync(issue.ProjectId, cancellationToken);
            if (pro == null)
                return null;
            var result = new IssueBuilder()
                .AddId(Guid.NewGuid().ToString())
                .AddDescription(issue.Description)
                .AddNumberCode(pro.Temp)
                .AddAssigneeId(issue.AssigneeId)
                .AddCreatedDate(issue.CreatedDate)
                .AddDueDate(issue.DueDate)
                .AddEnvironment(issue.Environment)
                .AddOriginEstimateTime(issue.OriginEstimateTime)
                .AddPriorityId(issue.PriorityId)
                .AddSeverityId(issue.SeverityId)
                .AddProjectId(issue.ProjectId)
                .AddRemainEstimateTime(issue.RemainEstimateTime)
                .AddReporterId(issue.ReporterId)
                .AddStatusId(issue.StatusId??pro.DefaultStatusId)
                .AddTitle(issue.Title)
                .Build();
            //increase after create new issue to generate code of issue
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
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var newStatus = await _unitOfWork
               .Status
               .GetByIdAsync(issue.StatusId, cancellationToken);
            var specificationResult =
                new IssueSpecification(issue.Id);
            var result = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            result
                .UpdateAssigneeId(issue.AssigneeId, issue.ModifierId, async log => await _unitOfWork.Issuelog.AddAsync(log));           
            result
                .UpdateDescription(issue.Description, issue.ModifierId, async log => await _unitOfWork.Issuelog.AddAsync(log));
            result
                .UpdateEnvironment(issue.Environment, issue.ModifierId, async log => await _unitOfWork.Issuelog.AddAsync(log));
            result
                .UpdateDueDate(issue.DueDate, issue.ModifierId, async log => await _unitOfWork.Issuelog.AddAsync(log));
            result
                .UpdateOriginalEstimateTime(issue.OriginEstimateTime, issue.ModifierId, async log => await _unitOfWork.Issuelog.AddAsync(log));
            result
                .UpdatePriorityId(issue.PriorityId, issue.ModifierId, async log => await _unitOfWork.Issuelog.AddAsync(log));
            result
                .UpdateSeverityId(issue.SeverityId, issue.ModifierId, async log => await _unitOfWork.Issuelog.AddAsync(log));           
            result
                .UpdateRemainEstimateTime(issue.RemainEstimateTime, issue.ModifierId, async log => await _unitOfWork.Issuelog.AddAsync(log));
            result
                .UpdateReporterId(issue.ReporterId, issue.ModifierId, async log => await _unitOfWork.Issuelog.AddAsync(log));           
            result
                .UpdateStatusId(newStatus, issue.ModifierId, async log => await _unitOfWork.Issuelog.AddAsync(log));            
            result
                .UpdateTitle(issue.Title, issue.ModifierId, async log=> await _unitOfWork.Issuelog.AddAsync(log));
            
            _unitOfWork.Save();
        }       

        public async Task UpdateTagsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var tags = issue
                .Tags
                .Select(t => 
                {
                    if(t.Id == 0)
                        return new Tag(t.Id, t.Name, t.Description, t.Color, t.CategoryId);
                    return _unitOfWork.Tag.GetByIdAsync(t.Id).Result;
                })
                .ToList();
            var result = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(new IssueSpecification(issue.Id), cancellationToken);
            
            result.UpdateTags(tags, issue.ModifierId, async log => await _unitOfWork.Issuelog.AddAsync(log));

            _unitOfWork.Save();
        }


        public async Task AddRelationOfIssue
            (RelationNormalDto relation,
            CancellationToken cancellationToken = default)
        {
            var result = new Relation(relation.Description, relation.TagId, relation.FromIssueId, relation.ToIssueId);
            var log = new IssuelogBuilder()
                .AddIssueId(relation.FromIssueId)
                .AddModifierId(relation.ModifierId)
                .AddTagId(1)
                .AddOldToIssueId(null)
                .AddNewToIssueId(relation.ToIssueId)
                .Build();
            await _unitOfWork.Issuelog.AddAsync(log, cancellationToken);
            await _unitOfWork.Relation.AddAsync(result, cancellationToken);

            _unitOfWork.Save();
        }

        public async Task DeleteRelationOfIssue
            (RelationNormalDto relation,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Relation
                .GetByIdAsync(new object[] { relation.FromIssueId, relation.ToIssueId, relation.TagId }, cancellationToken);
            var log = new IssuelogBuilder()
                 .AddIssueId(relation.FromIssueId)
                 .AddModifierId(relation.ModifierId)
                 .AddTagId(1)
                 .AddOldToIssueId(relation.ToIssueId)
                 .AddNewToIssueId(null)
                 .Build();
            await _unitOfWork.Issuelog.AddAsync(log, cancellationToken);
            _unitOfWork.Relation.Delete(result);
            _unitOfWork.Save();
        }

        public async Task UpdateAttachmentsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var attachments = issue.Attachments
                .Select(a => new Attachment(a.Id, a.Uri, a.IssueId))
                .ToList();
            await _unitOfWork
                .Issue
                .UpdateAttachmentsOfIssueAsync(issue.Id, attachments, cancellationToken);
            var log = new IssuelogBuilder()
                 .AddIssueId(issue.Id)
                 .AddModifierId(issue.ModifierId)
                 .AddTagId(1)
                 .Build();
            await _unitOfWork.Issuelog.AddAsync(log, cancellationToken);
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
