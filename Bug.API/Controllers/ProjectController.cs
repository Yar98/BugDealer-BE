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
        [JwtFilter(Permission = 1, ProjectId = "project1")]
        public string Get()
        {
            Response.Headers.Add("yarito", "gaming");
            return "Ok";
        }

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(string projectId)
        {
            var result = await _projectService
                .GetNormalProjectAsync(projectId);
            return Ok(Bts.ConvertJson(result));
        }

        // GET api/Project/detail/5
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetailProject(string id)
        {
            var result = await _projectService
                .GetDetailProjectAsync(id);
            return Ok(Bts.ConvertJson(result));
        }

        // GET: api/Project/paging/1/account1/1/1/3/id
        [HttpGet("paging/creator/{accountId}/{tagId:int}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedProjectsByCreatorIdTagId
            (string accountId,
            int pageIndex, 
            int pageSize,
            int tagId,
            string sortOrder)
        {
            var result = 
                await _projectService.GetPaginatedByCreatorIdTagIdAsync(
                    accountId, pageIndex, pageSize, tagId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("paging/member/{accountId}/{tagId:int}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedProjectsByMemberIdTagId
            (string accountId,
            int pageIndex,
            int pageSize,
            int tagId,
            string sortOrder)
        {
            var result =
                await _projectService.GetPaginatedByMemberIdTagIdAsync(
                    accountId, pageIndex, pageSize, tagId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // GET: api/Project/offset/1/account1/1/1/3/id
        [HttpGet("offset/creator/{accountId}/{tagId:int}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextProjectsByOffsetByCreatorIdTagId
            (string accountId,
            int offset,
            int next,
            int tagId,
            string sortOrder)
        {
            var result =
                await _projectService.GetNextByOffsetByCreatorIdTagIdAsync(
                    accountId, offset, next, tagId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // GET: api/Project/offset/1/account1/Open/1/3/id
        [HttpGet("offset/member/{accountId}/{tagId:int}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextProjectsByOffsetByMemberIdTagId
            (string accountId,
            int offset,
            int next,
            int tagId,
            string sortOrder)
        {
            var result =
                await _projectService.GetNextByOffsetByMemberIdTagIdAsync(
                    accountId, offset, next, tagId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // POST api/Project
        [HttpPost]
        public async Task<IActionResult> PostAddProject
            ([FromBody] ProjectNormalDto pro)
        {
            var result = await _projectService.AddProjectAsync(pro);

            return CreatedAtAction(
                nameof(GetDetailProject), new { id = result.Id }, Bts.ConvertJson(result,4));
        }

        // PUT api/Project/detail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpdateDetailProject
            (string id, 
            [FromBody] ProjectNormalDto pro)
        {
            if (id != pro.Id)
                return BadRequest();
            await _projectService.UpdateBasicProjectAsync(pro);
            return NoContent();
        }

        [HttpPut("{projectId}/updateroles")]
        public async Task<IActionResult> PutUpdateRolesOfProject
            (string projectId,
            [FromBody] ProjectNormalDto pro)
        {
            if (projectId != pro.Id)
                return BadRequest();
            await _projectService.UpdateRolesOfProjectAsync(pro);
            return NoContent();
        }

        [HttpPut("{projectId}/add/role/{roleId:int}")]
        public async Task<IActionResult> PutAddRoleToProject
            (string projectId,
            int roleId)
        {
            await _projectService.AddRoleToProjectAsync(projectId, roleId);
            return NoContent();
        }

        [HttpPut("{projectId}/updatestatuses")]
        public async Task<IActionResult> PutUpdateStatusesOfProject
            (string projectId,
            [FromBody] ProjectNormalDto pro)
        {
            if (projectId != pro.Id)
                return BadRequest();
            await _projectService.UpdateStatusesOfProjectAsync(pro);
            return NoContent();
        }

        // DELETE api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }


    }
}
