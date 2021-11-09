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
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        // GET: api/<PermissionController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _permissionService.GetAllAsync();
            return Ok(result);
        }

        // GET api/<PermissionController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermissionById(int id)
        {
            var result = await _permissionService.GetPermissionByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("project/{projectId}/account/{memberId}")]
        public async Task<IActionResult> GetPermissionsByProjectAccount
            (string projectId,
            string memberId)
        {
            var result = await _permissionService
                .GetPermissionsByAccountProjectAsync(memberId, projectId);
            return Ok(result);
        }

        [HttpGet("project/{projectId}/role/{roleId}")]
        public async Task<IActionResult> GetPermissionsByProjectRole
            (string projectId,
            string roleId)
        {
            var result = await _permissionService
                .GetPermissionsByRoleProjectAsync(roleId, projectId);
            return Ok(result);
        }

    }
}
