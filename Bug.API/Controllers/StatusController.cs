using Bug.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Core.Common;
using Bug.API.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bug.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }
        // GET: api/<StatusController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StatusController>/5
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetailStatusById(string id)
        {
            var result = await _statusService.GetDetailStatusByIdAsync(id);
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
            var result = await _statusService
                .GetPaginatedByProjectIdSearch(projectId, search, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("search/paging/creator/{creatorId}/{search}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedStatusesByCreatorId
            (string creatorId,
            string search,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _statusService
                .GetPaginatedByCreatorIdSearch(creatorId, search, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("paging/creator/{creatorId}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedStatusesByCreator
            (string creatorId,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _statusService
                .GetPaginatedDetailByCreatorIdAsync(creatorId, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("offset/creator/{creatorId}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextStatusesByOffsetByCreator
            (string creatorId,
            int offset,
            int next,
            string sortOrder)
        {
            var result = await _statusService
                .GetNextByOffsetDetailByCreatorIdAsync(creatorId, offset, next, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("paging/project/{projectId}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedStatusesByProjectId
            (string projectId,
            string creatorId,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _statusService
                .GetPaginatedDetailByCreatorIdProjectIdAsync(projectId, creatorId, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("offset/project/{projectId}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextByOffsetStatusesByProjectId
            (string projectId,
            string creatorId,
            int offset,
            int next,
            string sortOrder)
        {
            var result = await _statusService
                .GetNextByOffsetDetailByCreatorIdProjectIdAsync(projectId, creatorId, offset, next, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("creator/{creatorId}/{sortOrder}")]
        public async Task<IActionResult> GetStatusesByCreatorId(string creatorId, string sortOrder)
        {
            var result = await _statusService
                .GetStatusesByCreatorIdAsync(creatorId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("project/{projectId}/{sortOrder}")]
        public async Task<IActionResult> GetStatusesByProjectId(string projectId, string sortOrder)
        {
            var result = await _statusService
                .GetStatusesByProjectIdAsync(projectId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // POST api/<StatusController>
        [HttpPost]
        public async Task<IActionResult> PostAddStatus([FromBody] StatusNormalDto st)
        {
            var result = await _statusService.AddStatusAsync(st);
            return CreatedAtAction(
                nameof(GetDetailStatusById),new { id = result.Id }, Bts.ConvertJson(result));
        }

        // PUT api/<StatusController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpdateStatus
            (string id, 
            [FromBody] StatusNormalDto st)
        {
            if (id != st.Id)
                return BadRequest();
            await _statusService.UpdateStatusAsync(st);
            return NoContent();
        }

        // DELETE api/<StatusController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(string id)
        {
            await _statusService.DeleteStatusAsync(id);
            return NoContent();
        }
    }
}
