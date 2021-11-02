using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.API.Utils;
using Bug.API.Dto;
using Bug.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bug.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJwtUtils _jwtUtils;
        public AccountController(IAccountService accountService, IJwtUtils jwtUtils)
        {
            _accountService = accountService;
            _jwtUtils = jwtUtils;
        }

        // GET: api/Account
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/Account/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(string id)
        {
            var result = await _accountService.GetAccountById(id);
            return Ok(result);
        }

        // POST api/Account/login
        [HttpPost("login")]
        public async Task<IActionResult> PostLoginBts([FromBody] AccountBtsLoginDto user)
        {
            var result = await _accountService.GetAccountByUserName(user.UserName, user.Password);
            if (result == null)
            {
                return BadRequest("account not exist");
            }
            return CreatedAtAction(
                nameof(GetAccountById), new { id = result.Id }, result);
            
        }

        // POST api/Account/register
        [HttpPost("register")]
        public async Task<IActionResult> PostRegisterBts([FromBody] AccountBtsRegister user)
        {
            var result = await _accountService.AddRegistedAccount(user);
            return CreatedAtAction(
                nameof(GetAccountById), new { id = result.Id }, result);
        }

        // PUT api/Account/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] AccountDetailDto user)
        {
            if (id != user.Id)
                return BadRequest();
            await _accountService.UpdateAccount(user);
            return NoContent();
        }

        // DELETE api/Account/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _accountService.DeleteAccount(id);
            return NoContent();
        }
    }
}
