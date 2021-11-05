using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Data.Infrastructure;
using Bug.API.Services;
using Bug.Core.Common;
using Bug.API.Dto;
using Bug.Entities.Model;
using Bug.API.ActionFilter;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bug.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [JwtFilter]
        public string Get()
        {
            Response.Headers.Add("yarito", "gaming");
            return "Ok";
        }

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(string projectId)
        {
            var result = await _projectService.GetNormalProject(projectId);
            return Ok(Bts.ConvertJson(result));
        }

        // GET api/Project/detail/5
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetailProject(string id)
        {
            var result = await _projectService
                .GetDetailProject(id);
            return Ok(Bts.ConvertJson(result));
        }

        // GET: api/Project/paging/1/account1/Open/1/3/id
        [HttpGet("paging/creator/{accountId}/{tagName}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedProjectsByCreator(
            string accountId,
            int pageIndex, 
            int pageSize,
            string tagName,
            string sortOrder)
        {
            var result = 
                await _projectService.GetPaginatedByCreator(
                    accountId, pageIndex, pageSize, Bts.ProjectTag, tagName, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("paging/member/{accountId}/{tagName}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedProjectsByMember(
            string accountId,
            int pageIndex,
            int pageSize,
            string tagName,
            string sortOrder)
        {
            var result =
                await _projectService.GetPaginatedByMember(
                    accountId, pageIndex, pageSize, Bts.ProjectTag, tagName, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // GET: api/Project/offset/1/account1/Open/1/3/id
        [HttpGet("offset/creator/{accountId}/{tagName}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextProjectsByOffsetByCreator(
            string accountId,
            int offset,
            int next,
            string tagName,
            string sortOrder)
        {
            var result =
                await _projectService.GetNextByOffsetByCreator(
                    accountId, offset, next, Bts.ProjectTag, tagName, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // GET: api/Project/offset/1/account1/Open/1/3/id
        [HttpGet("offset/member/{accountId}/{tagName}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextProjectsByOffsetByMember(
            string accountId,
            int offset,
            int next,
            string tagName,
            string sortOrder)
        {
            var result =
                await _projectService.GetNextByOffsetByMember(
                    accountId, offset, next, Bts.ProjectTag, tagName, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // POST api/Project
        [HttpPost]
        public async Task<IActionResult> PostCreateProject([FromBody] ProjectNormalDto pro)
        {
            var result = await _projectService.AddProject(pro);
            return CreatedAtAction(
                nameof(GetProject), new { id = result.Id }, pro);
        }

        // PUT api/Project/detail/5
        [HttpPut("detail/{id}")]
        public async Task<IActionResult> PutEditDetailProject(
            string id, 
            [FromBody] ProjectNormalDto pro)
        {
            if (id != pro.Id)
                return BadRequest();
            await _projectService.UpdateProject(pro);
            return NoContent();
        }

        // DELETE api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            await _projectService.DeleteProject(id);
            return NoContent();
        }


    }
}
