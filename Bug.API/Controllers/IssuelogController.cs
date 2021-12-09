using Bug.API.Dto;
using Bug.API.Services;
using Bug.API.SignalR;
using Bug.Core.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuelogController : ControllerBase
    {
        private readonly IIssuelogService _issuelogService;
        private readonly IHubContext<ChatHub, IChatClient> _hubContext;

        public IssuelogController(IIssuelogService issuelogService, IHubContext<ChatHub, IChatClient> hubContext)
        {
            _issuelogService = issuelogService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            await _hubContext.Clients.Group("group").ReceiveMessage("issue1","hihihi");
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssuelogById(int id)
        {
            var result = await _issuelogService.GetDetailIssuelogByIdAsync(id);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("issue/{issueId}")]
        public async Task<IActionResult> GetIssuelogsByIssueId(string issueId)
        {
            var result = await _issuelogService
                .GetIssuelogsByIssueIdAsync(issueId);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("issue/{issueId}/tag/{tagId:int}")]
        public async Task<IActionResult> GetIssuelogsByIssueIdTagId
            (string issueId,
            int tagId)
        {
            var result = await _issuelogService
                .GetIssuelogsByIssueIdTagIdAsync(issueId, tagId);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("issue/{issueId}/category/{categoryId:int}")]
        public async Task<IActionResult> GetIssuelogsByIssueIdCategoryId
            (string issueId,
            int categoryId)
        {
            var result = await _issuelogService
                .GetIssuelogsByIssueIdCategoryIdAsync(issueId, categoryId);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpPost]
        public async Task<IActionResult> PostAddIssuelog([FromBody] IssuelogNormalDto ilog)
        {
            var result = await _issuelogService
                .AddIssuelogAsync(ilog);
            await _hubContext
                .Clients
                .Group(result.Issue.Id)
                .ReceiveMessage(result.Issue.Id, Bts.ConvertJson(result));
            return CreatedAtAction(
                nameof(GetIssuelogById), new { id = result.Id }, Bts.ConvertJson(result));
        }
    }
}
