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
        [JwtFilter(Permission = 1)]
        public string Get()
        {
            Response.Headers.Add("yarito", "gaming");
            return "Ok";
        }

        // GET api/Project/detail/5
        [HttpGet("detail/{projectId}/modifier/{modifierId}")]
        public async Task<IActionResult> GetDetailProject(string projectId, string modifierId)
        {
            var result = await _projectService
                .GetDetailProjectAsync(projectId, modifierId);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("search/paging/member/{memberId}/{state:int}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedByMemberIdSearch
            (string memberId,
            int state,
            string search,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _projectService
                .GetPaginatedByMemberIdSearchAsync(memberId, state, search, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("offset/account/{accountId}/{offset:int}/{next:int}")]
        public async Task<IActionResult> GetNextProjectsByOffsetByCreatorIdTagId
            (string accountId,
            int offset,
            int next)
        {
            var result =
                await _projectService.GetNextRecentByOffsetAsync(accountId, offset, next);
            return Ok(Bts.ConvertJson(result));
        }

        // GET: api/Project/paging/1/account1/1/1/3/id
        [HttpGet("paging/creator/{accountId}/{state:int}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedProjectsByCreatorIdTagId
            (string accountId,
            int pageIndex, 
            int pageSize,
            int state,
            string sortOrder)
        {
            var result = 
                await _projectService
                .GetPaginatedByCreatorIdStatusAsync(accountId, pageIndex, pageSize, state, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // GET: api/Project/offset/1/account1/1/1/3/id
        [HttpGet("offset/creator/{accountId}/{state:int}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextProjectsByOffsetByCreatorIdTagId
            (string accountId,
            int offset,
            int next,
            int state,
            string sortOrder)
        {
            var result =
                await _projectService.GetNextByOffsetByCreatorIdTagIdAsync(
                    accountId, offset, next, state, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("paging/member/{accountId}/{state:int}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedProjectsByMemberIdTagId
            (string accountId,
            int pageIndex,
            int pageSize,
            int state,
            string sortOrder)
        {
            var result =
                await _projectService.GetPaginatedByMemberIdStateAsync(
                    accountId, pageIndex, pageSize, state, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // GET: api/Project/offset/1/account1/Open/1/3/id
        [HttpGet("offset/member/{accountId}/{state:int}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextProjectsByOffsetByMemberIdTagId
            (string accountId,
            int offset,
            int next,
            int state,
            string sortOrder)
        {
            var result =
                await _projectService.GetNextByOffsetByMemberIdStateAsync(
                    accountId, offset, next, state, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // POST api/Project       
        [ModelFilter]
        [ProjectFilter]
        [HttpPost]
        public async Task<IActionResult> PostAddProject
            ([FromBody] ProjectPostDto pro)
        {
            var result = await _projectService.AddProjectAsync(pro);

            return CreatedAtAction(
                nameof(GetDetailProject), new { projectId = result.Id }, Bts.ConvertJson(result,4));
        }

        // PUT api/Project/detail/5
        [HttpPut("{projectId}")]
        public async Task<IActionResult> PutUpdateBasicProject
            (string projectId, 
            [FromBody] ProjectPutDto pro)
        {
            if (projectId != pro.Id)
                return BadRequest();
            await _projectService.UpdateBasicProjectAsync(pro);
            return NoContent();
        }

        [HttpPut("{projectId}/updateroles")]
        public async Task<IActionResult> PutUpdateRolesOfProject
            (string projectId,
            [FromBody] ProjectPutDto pro)
        {
            if (projectId != pro.Id)
                return BadRequest();
            await _projectService.UpdateRolesOfProjectAsync(pro);
            return NoContent();
        }

        [HttpPut("{projectId}/updatestatuses")]
        public async Task<IActionResult> PutUpdateStatusesOfProject
            (string projectId,
            [FromBody] ProjectPutStatusesDto pro)
        {
            if (projectId != pro.Id)
                return BadRequest();
            await _projectService.UpdateStatusesOfProjectAsync(pro);
            return NoContent();
        }

        [HttpPut("add/account/to/project")]
        public async Task<IActionResult> PutAddMemberToProject([FromBody] AprPutDto apr)
        {
            await _projectService
                .AddMemberToProjectAsync(apr.AccountId, apr.ProjectId);
            return NoContent();
        }

        [HttpPut("delete/member")]
        public async Task<IActionResult> PutDeleteMemberFromProject
            ([FromBody] ProjectPutDto acc)
        {
            
            await _projectService
                .DeleteMemberFromProjectAsync(acc.Id, acc.AccountId);
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
