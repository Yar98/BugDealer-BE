using Bug.API.Services;
using Bug.Core.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityService _priorityService;
        public PriorityController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPriorities()
        {
            var result = await _priorityService.GetPrioritiesAsync();
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPriorityById(int id)
        {
            var result = await _priorityService.GetPriorityByIdAsync(id);
            return Ok(Bts.ConvertJson(result));
        }

    }
}
