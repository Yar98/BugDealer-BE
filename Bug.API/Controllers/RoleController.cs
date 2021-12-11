using Bug.API.Dto;
using Bug.Core.Common;
using Bug.API.Services;
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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/<RoleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RoleController>/5
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetailRole(int id)
        {
            var result =  await _roleService.GetDetailRoleByIdAsync(id);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("project/{projectId}/{sortOrder}")]
        public async Task<IActionResult> GetDetailRolesByProject
            (string projectId, string sortOrder)
        {
            var result = await _roleService
                .GetRolesByProjectIdAsync(projectId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("creator/{creatorId}/{sortOrder}")]
        public async Task<IActionResult> GetDetailRolesByCreator
            (string creatorId,
            string sortOrder)
        {
            var result = await _roleService
                .GetRolesByCreatorIdAsync(creatorId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("member/{memberId}/project/{projectId}/{sortOrder}")]
        public async Task<IActionResult> GetDetailRolesByMember
            (string projectId,
            string memberId,
            string sortOrder)
        {
            var result = await _roleService
                .GetRolesWhichMemberIdProjectIdOnAsync(projectId, memberId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("search/paging/project/{projectId}/{search}/{pageIndex}/{pageSize}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedByProjectIdSearch
            (string projectId,
            string search,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _roleService
                .GetPaginatedByProjectIdSearch(projectId, search, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("search/paging/creator/{creatorId}/{search}/{pageIndex}/{pageSize}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedByCreatorIdSearch
            (string creatorId,
            string search,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _roleService
                .GetPaginatedByCreatorIdSearch(creatorId, search, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("paging/project/{projectId}/member/{memberId}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedWhichMemberIdOn
            (string projectId,
            string memberId,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _roleService
                .GetPaginatedWhichMemberIdOnAsync(projectId, memberId, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("offset/project/{projectId}/member/{memberId}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextByOffsetWhichMemberIdOn
            (string projectId,
            string memberId,
            int offset,
            int next,
            string sortOrder)
        {
            var result = await _roleService
                .GetNextByOffsetWhichMemberIdOnAsync(projectId, memberId, offset, next, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("paging/creator/{creatorId}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedRolesByCreatorId
            (string creatorId,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _roleService
                .GetPaginatedByCreatorId(creatorId, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("offset/creator/{creatorId}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextRolesByOffsetByCreatorId
            (string creatorId,
            int offset,
            int next,
            string sortOrder)
        {
            var result = await _roleService
                .GetNextByOffsetByCreatorIdAsync(creatorId, offset, next, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // POST api/<RoleController>
        [HttpPost]
        public async Task<IActionResult> PostAddRole([FromBody] RoleNormalDto value)
        {
            var result = await _roleService.AddNewRoleAsync(value);
            return CreatedAtAction(
                nameof(GetDetailRole), new { id = result.Id }, Bts.ConvertJson(result));
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpdateRole(int id, [FromBody] RoleNormalDto value)
        {
            if (id != value.Id)
                return BadRequest();
            await _roleService.UpdateRoleAsync(value);
            return NoContent();
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleService.DeleteRoleAsync(id);
            return NoContent();
        }
    }
}
