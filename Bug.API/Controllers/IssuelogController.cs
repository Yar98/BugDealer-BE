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

        [HttpGet("issue/{issueId}/{sortOrder}")]
        public async Task<IActionResult> GetIssuelogsByIssueId(string issueId, string sortOrder)
        {
            var result = await _issuelogService
                .GetIssuelogsByIssueIdAsync(issueId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("issue/{issueId}/tag/{tagId:int}/{sortOrder}")]
        public async Task<IActionResult> GetIssuelogsByIssueIdTagId
            (string issueId,
            int tagId,
            string sortOrder)
        {
            var result = await _issuelogService
                .GetIssuelogsByIssueIdTagIdAsync(issueId, tagId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("issue/{issueId}/category/{categoryId:int}/{sortOrder}")]
        public async Task<IActionResult> GetIssuelogsByIssueIdCategoryId
            (string issueId,
            int categoryId,
            string sortOrder)
        {
            var result = await _issuelogService
                .GetIssuelogsByIssueIdCategoryIdAsync(issueId, categoryId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        
    }
}
