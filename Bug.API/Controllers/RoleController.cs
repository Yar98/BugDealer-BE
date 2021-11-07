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
        public async Task<IActionResult> GetDetailRole(string id)
        {
            var result =  await _roleService.GetDetailRoleByIdAsync(id);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("paging/project/{projectId}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedDetailRolesByProject
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _roleService
                .GetPaginatedDetailByProjectAsync(projectId, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("offset/project/{projectId}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextDetailRolesByOffsetByProject
            (string projectId,
            int offset,
            int next,
            string sortOrder)
        {
            var result = await _roleService
                .GetNextDetailByOffsetByProjectAsync(projectId, offset, next, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // POST api/<RoleController>
        [HttpPost]
        public async Task<IActionResult> PostAddRole([FromBody] RoleNormalDto value)
        {
            var result = await _roleService.AddRoleAsync(value);
            return CreatedAtAction(
                nameof(GetDetailRole), new { id = result.Id }, result);
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpdateRole(string id, [FromBody] RoleNormalDto value)
        {
            if (id != value.Id)
                return BadRequest();
            await _roleService.UpdateRoleAsync(value);
            return NoContent();
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            await _roleService.DeleteRoleAsync(id);
            return NoContent();
        }
    }
}
