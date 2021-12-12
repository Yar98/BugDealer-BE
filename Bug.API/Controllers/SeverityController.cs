using Bug.API.Services;
using Bug.Core.Common;
using Bug.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bug.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeverityController : ControllerBase
    {
        private readonly ISeverityService _severityService;
        private readonly IConfiguration _config;
        public SeverityController(ISeverityService severityService, IConfiguration config)
        {
            _severityService = severityService;
            _config = config;
        }

        // GET: api/<SeverityController>
        [HttpGet]
        public IActionResult Get()
        {
            var test = new AmazonS3Bts(_config).GeneratePreSignedURL(1);
            return Ok(Bts.ConvertJson(test));
        }

        // GET api/<SeverityController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _severityService
                .GetDetailByIdAsync(id);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _severityService
                .GetAllSeveritiesAsync();
            return Ok(Bts.ConvertJson(result));
        }

    }
}
