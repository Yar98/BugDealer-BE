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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleProject(string accountId)
        {
            
            return Ok();
        }

        // GET: api/<ProjectController>/recent?accountId&count&tagType&nameTag
        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentProjects(string accountId,
            string nameTag,
            int count)
        {
            var result = await _projectService
                .GetRecentProjects(accountId, Bts.ProjectTag, nameTag, count);
            return StatusCode(200, result);
        }

        // GET api/<ProjectController>/detail/5
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetailProject(string id)
        {
            var result = await _projectService
                .GetDetailProject(id);
            return Ok(result);
        }

        // POST api/<ProjectController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectNewDto pro)
        {
            var result = await _projectService.CreateProject(pro);
            return CreatedAtAction(nameof(GetSingleProject), pro);
        }

        // PUT api/<ProjectController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}
