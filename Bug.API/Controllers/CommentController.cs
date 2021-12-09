using Bug.API.Dto;
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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        // GET: api/<CommentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var result = await _commentService.GetDetailCommentByIdAsync(id);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("issue/{issueId}/{sortOrder}")]
        public async Task<IActionResult> GetCommentsByIssueId(string issueId, string sortOrder)
        {
            var result = await _commentService.GetCommentsByIssueIdAsync(issueId, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        // POST api/<CommentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CommentNormalDto cmt)
        {
            var result = await _commentService.AddCommentAsync(cmt);
            return CreatedAtAction(
                nameof(GetCommentById), new { id = result.Id }, Bts.ConvertJson(result));
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpdateCommentById
            (int id, [FromBody] CommentNormalDto cmt)
        {
            if (id != cmt.Id)
                return BadRequest();
            await _commentService.UpdateCommentByIdAsync(cmt);
            return NoContent();
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentById(int id)
        {
            await _commentService.DeleteCommentByIdAsync(id);
            return NoContent();
        }
    }
}
