using Bug.API.Services;
using Bug.Core.Common;
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
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: api/<TagController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TagController>/5
        [HttpGet("{id}/detail")]
        public async Task<IActionResult> GetDetailTagById(int id)
        {
            var result = await _tagService
                .GetDetailTagByIdAsync(id);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("category/{categoryId:int}/{sortOrder}")]
        public async Task<IActionResult> GetTagsByCategory(int categoryId, string sortOrder)
        {
            var result = await _tagService
                .GetTagsByCategoryIdAsync(categoryId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("project/{projectId}/category/{categoryId:int}/{sortOrder}")]
        public async Task<IActionResult> GetTagsByCategoryIdProjectId
            (string projectId,
            int categoryId,
            string sortOrder)
        {
            var result = await _tagService
                .GetTagsByCategoryIdProjectIdAsync(projectId, categoryId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

    }
}
