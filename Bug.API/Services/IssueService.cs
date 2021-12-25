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
using Bug.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Bug.Core.Common;

namespace Bug.API.Services
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        
        public IssueService(IUnitOfWork uow, IConfiguration config)
        {
            _unitOfWork = uow;
            _config = config;
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

        public async Task<IReadOnlyList<Issuelog>> GetNextRecentByOffsetAsync
            (string accountId,
            int offset,
            int next,
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork
                .Issuelog
                .GetRecentAsync(accountId, offset, next, cancellationToken);
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
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
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
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
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
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
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
            var tasks = new List<Task>();

            tasks.Add(CreateTagsOfIssueAsync(result, issue.Tags));
            tasks.Add(CreateAttachmentsOfIssueAsync(result, issue.Attachments));
            tasks.Add(CreateRelationsOfIssueAsync(result, issue.ModifierId, issue.FromRelations));
            await Task.WhenAll(tasks);

            var log = new IssuelogBuilder()
                    .AddIssueId(result.Id)
                    .AddModifierId(issue.ModifierId)
                    .AddTagId(Bts.LogCreateIssueTag)
                    .Build();

            await _unitOfWork.Issuelog.AddAsync(log, cancellationToken);
            await _unitOfWork.Issue.AddAsync(result,cancellationToken);
            
            _unitOfWork.Save();

            if (result.Attachments.Count >= 1)
            {
                var key = result.Attachments
                    .Select(a => a.Id.ToString())
                    .ToList()
                    .Aggregate((x, y) => x + "_" + y);
                result.PresignLink = new AmazonS3Bts(_config)
                    .GenerateUploadPreSignedURL("bugdealer", key);
            }
            return result;
        }

        public async Task UpdateIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var newStatus = await _unitOfWork
               .Status
               .GetByIdAsync(issue.StatusId, cancellationToken);
            var result = await _unitOfWork
                .Issue
                .GetByIdAsync(issue.Id, cancellationToken);
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

        public async Task AddWatcherToIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(new IssueSpecification(issue.Id, 2), cancellationToken);
            var user = await _unitOfWork
                .Account
                .GetByIdAsync(issue.WatcherId, cancellationToken);
            result.Watchers.Add(user);

            _unitOfWork.Save();
        }

        public async Task AddVoterToIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(new IssueSpecification(issue.Id, 3), cancellationToken);
            var user = await _unitOfWork
                .Account
                .GetByIdAsync(issue.VoterId, cancellationToken);
            result.Voters.Add(user);

            _unitOfWork.Save();
        }

        public async Task DeleteWatcherToIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(new IssueSpecification(issue.Id, 2), cancellationToken);
            var user = await _unitOfWork
                .Account
                .GetByIdAsync(issue.WatcherId, cancellationToken);
            result.Watchers.Remove(user);

            _unitOfWork.Save();
        }

        public async Task DeleteVoterToIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(new IssueSpecification(issue.Id, 3), cancellationToken);
            var user = await _unitOfWork
                .Account
                .GetByIdAsync(issue.VoterId, cancellationToken);
            result.Voters.Add(user);

            _unitOfWork.Save();
        }

        public async Task UpdateTagsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(new IssueSpecification(issue.Id, 1), cancellationToken);
            await Task.WhenAll
                (DeleteLocalTagsOfIssueAsync(result,issue),
                AddLocalTagsOfIssueAsync(result,issue));

            var log = new IssuelogBuilder()
                    .AddIssueId(issue.Id)
                    .AddModifierId(issue.ModifierId)
                    .AddTagId(Bts.LogUpdateLabelTag)
                    .Build();
            await _unitOfWork.Issuelog.AddAsync(log, cancellationToken);

            _unitOfWork.Save();
        }

        public async Task AddRelationOfIssue
            (RelationNormalDto relation,
            CancellationToken cancellationToken = default)
        {
            var tagDes = await _unitOfWork.Tag.GetByIdAsync(relation.TagId, cancellationToken);
            var result = new Relation(relation.Description, relation.TagId, relation.FromIssueId, relation.ToIssueId);
            var reverseResult = CreateReverseRelation(result);
            if(reverseResult != null)
            {
                var reverseTagDes = await _unitOfWork.Tag.GetByIdAsync(reverseResult.TagId, cancellationToken);
                var reverselog = new IssuelogBuilder()
                .AddIssueId(reverseResult.FromIssueId)
                .AddModifierId(relation.ModifierId)
                .AddTagId(Bts.LogUpdateLinkTag)
                .AddOldToIssueId(null)
                .AddNewToIssueId(relation.FromIssueId)
                .AddDescription(reverseTagDes.Name)
                .Build();
                await _unitOfWork.Issuelog.AddAsync(reverselog, cancellationToken);
                await _unitOfWork.Relation.AddAsync(reverseResult, cancellationToken);
            }
            var log = new IssuelogBuilder()
                .AddIssueId(relation.FromIssueId)
                .AddModifierId(relation.ModifierId)
                .AddTagId(Bts.LogUpdateLinkTag)
                .AddOldToIssueId(null)
                .AddNewToIssueId(relation.ToIssueId)
                .AddDescription(tagDes.Name)
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
            var reverseResult = await GetReverseRelation(result, cancellationToken);
            var tagDes = await _unitOfWork.Tag.GetByIdAsync(relation.TagId,cancellationToken);
            if(reverseResult != null)
            {
                var reverseTagDes = _unitOfWork.Tag.GetById(reverseResult.TagId);
                var reverselog = new IssuelogBuilder()
                 .AddIssueId(reverseResult.FromIssueId)
                 .AddModifierId(relation.ModifierId)
                 .AddTagId(Bts.LogUpdateLinkTag)
                 .AddOldToIssueId(reverseResult.FromIssueId)
                 .AddNewToIssueId(null)
                 .AddDescription(reverseTagDes.Name)
                 .Build();
                await _unitOfWork.Issuelog.AddAsync(reverselog, cancellationToken);
                _unitOfWork.Relation.Delete(reverseResult);
            }
            
            var log = new IssuelogBuilder()
                 .AddIssueId(relation.FromIssueId)
                 .AddModifierId(relation.ModifierId)
                 .AddTagId(Bts.LogUpdateLinkTag)
                 .AddOldToIssueId(relation.ToIssueId)
                 .AddNewToIssueId(null)
                 .AddDescription(tagDes.Name)
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
                 .AddTagId(Bts.LogUpdateAttachmentTag)
                 .Build();
            await _unitOfWork.Issuelog.AddAsync(log, cancellationToken);
        }

        public async Task DeleteIssueAsync
            (string id, 
            CancellationToken cancellationToken = default)
        {
            await _unitOfWork
                .Relation
                .DeleteRelationByIssueAsync(id,cancellationToken);
            await _unitOfWork
                .Issuelog
                .DeleteLogBeforeDelIssue(id, cancellationToken);
            await _unitOfWork
                .Issue
                .DeleteIssueById(id, cancellationToken);
        }

        private Task DeleteLocalTagsOfIssueAsync(Issue dbIssue, IssueNormalDto inputIssue)
        {
            foreach (var t in dbIssue.Tags.ToList())
            {
                var tag = inputIssue.Tags.FirstOrDefault(ta => ta.Id == t.Id);
                if (tag == null)
                    dbIssue.RemoveTag(t);
            }

            return Task.CompletedTask;
        }

        private Task AddLocalTagsOfIssueAsync(Issue dbIssue, IssueNormalDto inputIssue)
        {
            foreach (var t in inputIssue.Tags)
            {
                var tag = dbIssue.Tags.FirstOrDefault(ta => ta.Id == t.Id);
                if (t.Id != 0 && tag == null)
                {
                    var tem = new Tag(t.Id, t.Name, t.Description, t.Color, t.CategoryId);
                    _unitOfWork.Tag.Unchange(tem);
                    dbIssue.AddTag(tem);
                }
                if (t.Id == 0)
                    dbIssue.AddTag(new Tag(t.Id, t.Name, t.Description, t.Color, t.CategoryId));
            }

            return Task.CompletedTask;
        }

        private Relation CreateReverseRelation(Relation input)
        {
            switch (input.TagId)
            {
                case 5:
                    return new Relation(null, 6, input.ToIssueId, input.FromIssueId);
                case 7:
                    return new Relation(null, 8, input.ToIssueId, input.FromIssueId);
                case 9:
                    return new Relation(null, 10, input.ToIssueId, input.FromIssueId);
                default:
                    return null;
            }
        }

        private async Task<Relation> GetReverseRelation
            (Relation input,
            CancellationToken cancellationToken = default)
        {
            var keys = Array.Empty<object>();
            switch (input.TagId)
            {
                case 5:
                    keys = new object[] { input.ToIssueId, input.FromIssueId, 6 };
                    break;
                case 7:
                    keys = new object[] { input.ToIssueId, input.FromIssueId, 8 };
                    break;
                case 9:
                    keys = new object[] { input.ToIssueId, input.FromIssueId, 10 };
                    break;
                default:
                    return null;
            }

            return await _unitOfWork
                .Relation
                .GetByIdAsync(keys, cancellationToken);
        }

        private async Task CreateRelationsOfIssueAsync
            (Issue issue, 
            string modifierId,
            List<RelationNormalDto> relations,
            CancellationToken  cancellationToken = default)
        {
            foreach(var r in relations)
            {
                issue.AddToNewRelation(r.Description, r.TagId, r.FromIssueId, r.ToIssueId);
                var reverseResult = CreateReverseRelation(new Relation(r.Description, r.TagId, issue.Id, r.ToIssueId));
                if(reverseResult != null)
                {
                    var reverseTagDes = _unitOfWork.Tag.GetById(reverseResult.TagId);
                    var reverselog = new IssuelogBuilder()
                     .AddIssueId(reverseResult.FromIssueId)
                     .AddModifierId(modifierId)
                     .AddTagId(Bts.LogUpdateLinkTag)
                     .AddOldToIssueId(r.FromIssueId)
                     .AddNewToIssueId(null)
                     .AddDescription(reverseTagDes.Name)
                     .Build();
                    _unitOfWork.Issuelog.Add(reverselog);
                    _unitOfWork.Relation.Add(reverseResult);
                }
                
                var tagDes = _unitOfWork.Tag.GetById(r.TagId);
                var log = new IssuelogBuilder()
                     .AddIssueId(r.FromIssueId)
                     .AddModifierId(modifierId)
                     .AddTagId(Bts.LogUpdateLinkTag)
                     .AddOldToIssueId(r.ToIssueId)
                     .AddNewToIssueId(null)
                     .AddDescription(tagDes.Name)
                     .Build();               
                _unitOfWork.Issuelog.Add(log);               
            }
        }

        private async Task CreateTagsOfIssueAsync(Issue issue, List<TagNormalDto> tags)
        {
            foreach(var l in tags)
            {
                var item = new Tag(l.Id, l.Name, l.Description, l.Color, l.CategoryId);
                if (item.Id == 0)
                    _unitOfWork.Tag.Add(item);
                else
                    _unitOfWork.Tag.Attach(item);
                issue.AddTag(item);
            }
        }

        private async Task CreateAttachmentsOfIssueAsync(Issue issue, List<AttachmentNormalDto> atts)
        {
            foreach(var a in atts)
            {
                var item = new Attachment(a.Id, a.Uri, a.IssueId);
                if (item.Id == 0)
                    _unitOfWork.Attachment.Add(item);
                else
                    _unitOfWork.Attachment.Attach(item);
                issue.AddAttachment(item);
            }
        }
    }
}
