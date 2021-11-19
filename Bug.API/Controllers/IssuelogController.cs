using Bug.API.Dto;
using Bug.API.Services;
using Bug.Core.Common;
using Microsoft.AspNetCore.Mvc;
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
        public IssuelogController(IIssuelogService issuelogService)
        {
            _issuelogService = issuelogService;
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
            return CreatedAtAction(
                nameof(GetIssuelogById), new { id = result.Id }, Bts.ConvertJson(result));
        }
    }
}
