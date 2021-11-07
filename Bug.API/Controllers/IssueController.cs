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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bug.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;
        public IssueController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        // GET api/Issue/5
        //[ActionName(nameof(GetDetailIssue))]
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetailIssue(string id)
        {
            var result = await _issueService.GetDetailIssueAsync(id);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("paging/project/{projectId}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedIssueByProject
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _issueService
                .GetPaginatedDetailByProjectAsync(projectId, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("offset/project/{projectId}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetByOffsetIssueByProject
            (string projectId,
            int offset,
            int next,
            string sortOrder)
        {
            var result = await _issueService
                .GetNextDetailByOffsetByProjectAsync(projectId, offset, next, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // POST api/Issue
        [HttpPost]
        public async Task<IActionResult> PostAddIssue([FromBody] IssueNormalDto issue)
        {
            var result = await _issueService.AddIssueAsync(issue);
            return CreatedAtAction(
                nameof(GetDetailIssue), new { id = result.Id }, issue);
        }

        // PUT api/Issue/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpdateIssue
            (string id, 
            [FromBody] IssueNormalDto issue)
        {
            if (id != issue.Id)
                return BadRequest();
            await _issueService.UpdateIssueAsync(issue);
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
