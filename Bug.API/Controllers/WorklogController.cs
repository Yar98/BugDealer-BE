﻿using Bug.API.ActionFilter;
using Bug.API.Dto;
using Bug.API.Services;
using Bug.Core.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bug.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorklogController : ControllerBase
    {
        private readonly IWorklogService _worklogService;
        public WorklogController(IWorklogService ws)
        {
            _worklogService = ws;
        }

        // GET api/<WorklogController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailWorklog(int id)
        {
            var result = await _worklogService.GetDetailWorklogByIdAsync(id);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("all/issue/{issueId}")]
        public async Task<IActionResult> GetDetailWorklog
            (string issueId)
        {
            var result = await _worklogService
                .GetAllRecentByIssueIdAsync(issueId);
            return Ok(Bts.ConvertJson(result));
        }

        // POST api/<WorklogController>
        [JwtFilter(Permission = 8)]
        [HttpPost("issue/{issueId}")]
        public async Task<IActionResult> Post
            (string issueId, 
            [FromBody] WorklogPostDto worklog)
        {
            var result = await _worklogService
                .AddNewWorklogToIssueAsync(issueId, worklog);
            return CreatedAtAction(
                nameof(GetDetailWorklog), new {id = result.Id }, Bts.ConvertJson(result));
        }

        // DELETE api/<WorklogController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _worklogService.DeleteWorklogAsync(id);
            return NoContent();
        }
    }
}
