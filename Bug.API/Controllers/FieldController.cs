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
    public class FieldController : ControllerBase
    {
        private readonly IFieldService _fieldService;
        public FieldController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        // GET: api/<FieldController>
        [HttpGet]
        public async Task<IActionResult> GetAllFields()
        {
            var result = await _fieldService.GetAllFieldsAsync();
            return Ok(Bts.ConvertJson(result));
        }

        // GET api/<FieldController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFieldById(int id)
        {
            var result = await _fieldService.GetFieldByIdAsync(id);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("account/{accountId}/customtype/{customtypeId}")]
        public async Task<IActionResult> GetFieldsByAccount
            (string accountId,
            int customtypeId)
        {
            var result = await _fieldService.GetFieldsByAccountCustomtypeAsync(accountId, customtypeId);
            return Ok(Bts.ConvertJson(result));
        }


    }
}
