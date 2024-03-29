﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Bug.API.Services;
using Bug.Core.Common;
using Bug.Entities.Model;
using Bug.API.Dto;
using Microsoft.AspNetCore.SignalR;
using Bug.API.SignalR;
using System.IO;
using Bug.API.ActionFilter;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bug.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;
        public IssueController
            (IIssueService issueService)
        {
            _issueService = issueService;
        }

        [JwtFilter]
        [HttpGet("export/{issueId}")]
        public async Task<IActionResult> GetExportIssue(string issueId)
        {
            var result = await _issueService
                .GetDetailIssueAsync(issueId,null);
            var stream = await _issueService
                .ExportIssueExcelFile(issueId);
            // Tạo buffer memory stream để hứng file excel
            var buffer = stream as MemoryStream;
            // chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            Response.Headers.Add("Content-Disposition", "attachment; filename=ExcelDemo.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            return File(buffer.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                result.Code+".xlsx");
        }

        [JwtFilter]
        [HttpGet("detail/{issueId}")]
        public async Task<IActionResult> GetDetailIssue(string issueId)
        {
            var result = await _issueService.GetDetailIssueAsync(issueId);
            return Ok(Bts.ConvertJson(result));
        }

        //[ActionName(nameof(GetDetailIssue))]
        [JwtFilter]
        [HttpGet("detail/{issueId}/{accountId}")]
        public async Task<IActionResult> GetDetailIssueAndLog(string issueId, string accountId)
        {
            var result = await _issueService.GetDetailIssueAsync(issueId, accountId);
            return Ok(Bts.ConvertJson(result));
        }

        [JwtFilter]
        [HttpGet("offset/account/{accountId}/{offset:int}/{next:int}")]
        public async Task<IActionResult> GetNextByOffsetIssuesByRelateUser
            (string accountId,
            int offset,
            int next)
        {
            var result = await _issueService
                .GetNextRecentByOffsetAsync(accountId, offset, next);
            return Ok(Bts.ConvertJson(result));
        }

        [JwtFilter]
        [HttpGet("filter/paging/project/{projectId}/{pageIndex}/{pageSize}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedIssuesByFilter
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var search = "%" + Request.Query["search"].ToString() + "%";
            var statuses = Request.Query["statuses"].ToString();
            var assignees = Request.Query["assignees"].ToString();
            var reporters = Request.Query["reporters"].ToString();
            var priorities = Request.Query["priorities"].ToString();
            var severities = Request.Query["severities"].ToString();
            var result = await _issueService
                .GetPaginatedByFilter(projectId, pageIndex, pageSize, sortOrder, search, statuses, assignees, reporters, priorities, severities);
            return Ok(Bts.ConvertJson(result));
        }

        [JwtFilter]
        [HttpGet("paging/relate-user/{accountId}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedIssuesByRelateUser
            (string accountId,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _issueService
                .GetPaginatedByRelateUserAsync(accountId, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [JwtFilter]
        [HttpGet("offset/relate-user/{accountId}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextByOffsetIssuesByRelateUser
            (string accountId,
            int offset,
            int next,
            string sortOrder)
        {
            var result = await _issueService
                .GetNextByOffsetByRelateUserAsync(accountId, offset, next, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [JwtFilter]
        [HttpGet("paging/project/{projectId}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedIssuesByProject
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _issueService
                .GetPaginatedDetailByProjectIdAsync(projectId, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [JwtFilter]
        [HttpGet("offset/project/{projectId}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextByOffsetIssuesByProject
            (string projectId,
            int offset,
            int next,
            string sortOrder)
        {
            var result = await _issueService
                .GetNextDetailByOffsetByProjectIdAsync(projectId, offset, next, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [JwtFilter]
        [HttpGet("paging/project/{projectId}/reporter/{reporterId}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedIssuesByReporterId
            (string projectId,
            string reporterId,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _issueService
                .GetPaginatedDetailByProjectIdReporterIdAsync(projectId, reporterId, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [JwtFilter]
        [HttpGet("offset/project/{projectId}/reporter/{reporterId}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextByOffsetByReporterId
            (string projectId,
            string reporterId,
            int offset,
            int next,
            string sortOrder)
        {
            var result = await _issueService
                .GetNextDetailByOffsetByProjectIdReporterIdAsync(projectId, reporterId, offset, next, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [JwtFilter]
        [HttpGet("paging/project/{projectId}/assignee/{assigneeId}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedIssuesByAssigneeId
            (string projectId,
            string assigneeId,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _issueService
                .GetPaginatedDetailByProjectIdAssigneeIdAsync(projectId,assigneeId, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [JwtFilter]
        [HttpGet("offset/project/{projectId}/assignee/{assigneeId}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextByOffsetByAssigneeId
            (string projectId,
            string assigneeId,
            int offset,
            int next,
            string sortOrder)
        {
            var result = await _issueService
                .GetNextDetailByOffsetByProjectIdAssigneeIdAsync(projectId, assigneeId, offset, next, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [JwtFilter]
        [HttpGet("suggest/project/{projectId}/{sortOrder}")]
        public async Task<IActionResult> GetSuggestIssuesByCode
            (string projectId, 
            string sortOrder)
        {
            var search = Request.Query["searchText"].ToString();
            var issueId = Request.Query["exceptIssueId"].ToString();
            var result = await _issueService
                .GetSuggestIssueByCode(search, projectId , issueId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [JwtFilter]
        [HttpGet("search/paging/project/{projectId}/{pageIndex}/{pageSize}/{sortOrder}")]
        public async Task<IActionResult> GetIssuesByProjectIdSearch
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var search = Request.Query["searchText"].ToString()??"";
            var result = await _issueService
                .GetPaginatedByProjectIdSearchAsync(search,projectId,pageIndex,pageSize,sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [JwtFilter]
        [HttpGet("{issueId}/relations/{sortOrder}")]
        public async Task<IActionResult> GetRelationsOfIssue(string issueId, string sortOrder)
        {
            var result = await _issueService
                .GetRelationOfIssueAsync(issueId,sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // POST api/Issue
        [JwtFilter(Permission = 5)]
        [HttpPost]
        public async Task<IActionResult> PostAddIssue([FromBody] IssueNormalDto issue)
        {
            var result = await _issueService.AddIssueAsync(issue);
            return CreatedAtAction(
                nameof(GetDetailIssue), new { issueId = result.Id}, Bts.ConvertJson(result,2));
        }

        [JwtFilter(Permission = 6)]
        [HttpPost("clone")]
        public async Task<IActionResult> PostCloneIssue([FromBody] IssueNormalDto issue)
        {
            var result = await _issueService.AddIssueAsync(issue);
            return CreatedAtAction(
                nameof(GetDetailIssue), new { issueId = result.Id}, Bts.ConvertJson(result, 2));
        }

        // PUT api/Issue/5
        [JwtFilter(Permission = 8)]
        [HttpPut("{issueId}")]
        public async Task<IActionResult> PutUpdateBasicIssue
            (string issueId, 
            [FromBody] IssueNormalDto issue)
        {
            if (issueId != issue.Id)
                return BadRequest();
            await _issueService.UpdateIssueAsync(issue);
            return NoContent();
        }

        [JwtFilter(Permission = 8)]
        [HttpPut("{issueId}/labels")]
        public async Task<IActionResult> PutUpdateLabelsOfIssue
            (string issueId,
            [FromBody] IssueNormalDto issue)
        {
            if (issueId != issue.Id)
                return BadRequest();
            await _issueService.UpdateTagsOfIssue(issue);
            return NoContent();
        }

        [JwtFilter(Permission = 8)]
        [HttpPut("{issueId}/attachments")]
        public async Task<IActionResult> PutUpdateAttachmentsOfIssue
            (string issueId,
            [FromBody] IssueNormalDto issue)
        {
            if (issueId != issue.Id)
                return BadRequest();
            await _issueService.UpdateAttachmentsOfIssue(issue);
            return NoContent();
        }

        [JwtFilter(Permission = 8)]
        [HttpPut("{issueId}/add/relation")]
        public async Task<IActionResult> PutAddRelationToIssue
            (string issueId,
            [FromBody] RelationNormalDto r)
        {
            if (issueId != r.FromIssueId)
                return BadRequest();
            await _issueService.AddRelationOfIssue(r);
            return NoContent();
        }

        [JwtFilter(Permission = 8)]
        [HttpPut("{issueId}/delete/relation")]
        public async Task<IActionResult> PutDeleteRelationToIssue
            (string issueId,
            [FromBody] RelationNormalDto r)
        {
            if (issueId != r.FromIssueId)
                return BadRequest();
            await _issueService.DeleteRelationOfIssue(r);
            return NoContent();
        }

        [JwtFilter]
        [HttpPut("{issueId}/add/watcher")]
        public async Task<IActionResult> PutAddWatcherToIssue
            (string issueId,
            [FromBody] IssueNormalDto r)
        {
            if (issueId != r.Id)
                return BadRequest();
            await _issueService.AddWatcherToIssueAsync(r);
            return NoContent();
        }

        [JwtFilter]
        [HttpPut("{issueId}/delete/watcher")]
        public async Task<IActionResult> PutDeleteWatcherToIssue
            (string issueId,
            [FromBody] IssueNormalDto r)
        {
            if (issueId != r.Id)
                return BadRequest();
            await _issueService.DeleteWatcherToIssueAsync(r);
            return NoContent();
        }

        [JwtFilter]
        [HttpPut("{issueId}/add/voter")]
        public async Task<IActionResult> PutAddVoterToIssue
            (string issueId,
            [FromBody] IssueNormalDto r)
        {
            if (issueId != r.Id)
                return BadRequest();
            await _issueService.AddVoterToIssueAsync(r);
            return NoContent();
        }

        [JwtFilter]
        [HttpPut("{issueId}/delete/voter")]
        public async Task<IActionResult> PutDeleteVoterToIssue
            (string issueId,
            [FromBody] IssueNormalDto r)
        {
            if (issueId != r.Id)
                return BadRequest();
            await _issueService.DeleteVoterToIssueAsync(r);
            return NoContent();
        }

        // DELETE api/Issue/5
        [JwtFilter(Permission = 7)]
        [HttpDelete("{issueId}")]
        public async Task<IActionResult> DeleteIssue(string issueId)
        {
            await _issueService.DeleteIssueAsync(issueId);
            return NoContent();
        }
    }
}
