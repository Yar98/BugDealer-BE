using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Data.Infrastructure;
using Bug.API.Services;
using Bug.Core.Common;
using Bug.API.Services.DTO;
using Bug.Entities.Model;

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

        // GET: api/<ProjectController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(string projectId)
        {
            var result = await _projectService.GetNormalProject(projectId);
            return Ok(result);
        }

        // GET api/<ProjectController>/detail/5
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetailProject(string id)
        {
            var result = await _projectService
                .GetDetailProject(id);
            return Ok(result);
        }

        // GET: api/<ProjectController>/recent/creatorId/nameTag/count
        [HttpGet("recent/{creatorId}/{nameTag}/{count:int}")]
        public async Task<IActionResult> GetRecentProjects(string creatorId,
            string nameTag,
            int count)
        {
            var result = await _projectService
                .GetRecentProjects(creatorId, Bts.ProjectTag, nameTag, count);
            return StatusCode(200, result);
        }

        // GET: api/Project/paging/account1/Open/1/3/id
        [HttpGet("paging/{creatorId}/{tagName}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedProjects(string creatorId,
            int pageIndex, int pageSize,
            string tagName,
            string sortOrder)
        {
            var result = 
                await _projectService.GetPaginatedProjects(
                    creatorId, pageIndex, pageSize, Bts.ProjectTag, tagName, sortOrder);

            return Ok(result);
        }

        [HttpGet("offset/{creatorId}/{tagName}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextProjectsByOffset(
            string creatorId,
            int offset, int next,
            string tagName,
            string sortOrder)
        {
            var result =
                await _projectService.GetNextProjectsById(
                    creatorId, offset, next, Bts.ProjectTag, tagName, sortOrder);
            return Ok(result);
        }

        // POST api/<ProjectController>
        [HttpPost]
        public async Task<IActionResult> PostCreateProject([FromBody] ProjectNormalDto pro)
        {
            var result = await _projectService.CreateProject(pro);
            return CreatedAtAction(
                nameof(GetProject), new { id = result.Id }, pro);
        }

        // PUT api/<ProjectController>/detail/5
        [HttpPut("detail/{id}")]
        public async Task<IActionResult> PutEditDetailProject(
            string id, [FromBody] ProjectDetailDto pro)
        {
            if (id != pro.Id)
                return BadRequest();
            await _projectService.UpdateDetailProject(pro);
            return NoContent();
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            await _projectService.DeleteProject(id);
            return NoContent();
        }


    }
}
