using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("export/{issueId}")]
        public async Task<IActionResult> GetExportIssue(string issueId)
        {
            return Ok();
        }

        // GET api/Issue/5
        //[ActionName(nameof(GetDetailIssue))]
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetailIssue(string id)
        {
            var result = await _issueService.GetDetailIssueAsync(id);
            return Ok(Bts.ConvertJson(result));
        }

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

        [HttpGet("suggest/account/{accountId}/code/{code}/{sortOrder}")]
        public async Task<IActionResult> GetSuggestIssuesByCode
            (string accountId, 
            string code,
            string sortOrder)
        {
            var result = await _issueService
                .GetSuggestIssueByCode(code, accountId,sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // POST api/Issue
        [HttpPost]
        public async Task<IActionResult> PostAddIssue([FromBody] IssuePostDto issue)
        {
            var result = await _issueService.AddIssueAsync(issue);
            return CreatedAtAction(
                nameof(GetDetailIssue), new { id = result.Id }, Bts.ConvertJson(result,2));
        }

        // PUT api/Issue/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpdateBasicIssue
            (string id, 
            [FromBody] IssuePostDto issue)
        {
            if (id != issue.Id)
                return BadRequest();
            await _issueService.UpdateIssueAsync(issue);
            return NoContent();
        }



        [HttpPut("{id}/labels")]
        public async Task<IActionResult> PutUpdateLabelsOfIssue
            (string id,
            [FromBody] IssuePostDto issue)
        {
            if (id != issue.Id)
                return BadRequest();
            await _issueService.UpdateTagsOfIssue(issue);
            return NoContent();
        }

        [HttpPut("{id}/attachments")]
        public async Task<IActionResult> PutUpdateAttachmentsOfIssue
            (string id,
            [FromBody] IssuePostDto issue)
        {
            if (id != issue.Id)
                return BadRequest();
            await _issueService.UpdateAttachmentsOfIssue(issue);
            return NoContent();
        }

        [HttpPut("{id}/fromrelations")]
        public async Task<IActionResult> PutUpdateFromRelationOfIssue
            (string id,
            [FromBody] IssuePostDto issue)
        {
            if (id != issue.Id)
                return BadRequest();
            await _issueService.UpdateFromRelationsOfIssue(issue);
            return NoContent();
        }

        // DELETE api/Issue/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(string id)
        {
            await _issueService.DeleteIssueAsync(id);
            return NoContent();
        }
    }
}
