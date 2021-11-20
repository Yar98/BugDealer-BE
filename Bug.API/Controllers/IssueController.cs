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
        private readonly IHubContext<ChatHub, IChatClient> _strongChatHubContext;
        public IssueController
            (IIssueService issueService, 
            IHubContext<ChatHub, IChatClient> chatHubContext)
        {
            _issueService = issueService;
            _strongChatHubContext = chatHubContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _strongChatHubContext
                .Clients
                .All
                .ReceiveMessage("", "");
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
                nameof(GetDetailIssue), new { id = result.Id }, Bts.ConvertJson(result));
        }

        // PUT api/Issue/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpdateBasicIssue
            (string id, 
            [FromBody] IssueNormalDto issue)
        {
            if (id != issue.Id)
                return BadRequest();
            await _issueService.UpdateIssueAsync(issue);
            return NoContent();
        }

        [HttpPut("{id}/labels")]
        public async Task<IActionResult> PutUpdateLabelsOfIssue
            (string id,
            [FromBody] IssueNormalDto issue)
        {
            if (id != issue.Id)
                return BadRequest();
            await _issueService.UpdateTagsOfIssue(issue);
            return NoContent();
        }

        [HttpPut("{id}/attachments")]
        public async Task<IActionResult> PutUpdateAttachmentsOfIssue
            (string id,
            [FromBody] IssueNormalDto issue)
        {
            if (id != issue.Id)
                return BadRequest();
            await _issueService.UpdateAttachmentsOfIssue(issue);
            return NoContent();
        }

        [HttpPut("{id}/fromrelations")]
        public async Task<IActionResult> PutUpdateFromRelationOfIssue
            (string id,
            [FromBody] IssueNormalDto issue)
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
