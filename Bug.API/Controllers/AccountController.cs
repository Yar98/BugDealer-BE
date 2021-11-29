﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.API.Utils;
using Bug.API.Dto;
using Bug.API.Services;
using Bug.Core.Common;
using Bug.API.ActionFilter;

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
            var result = await _accountService.GetAccountByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("paging/project/{projectId}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedByProjectId
           (string projectId,
           int pageIndex,
           int pageSize,
           string sortOrder)
        {
            var result =
                await _accountService.GetPaginatedByProjectIdAsync(
                    projectId, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("offset/project/{projectId}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextByOffsetByProjectId
            (string projectId,
            int offset,
            int next,
            string sortOrder)
        {
            var result =
                await _accountService.GetNextByOffsetByProjectIdAsync(
                    projectId, offset, next, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }       

        // POST api/Account/login
        [HttpPost("login")]
        [ModelFilter]
        [AccountFilter]
        public async Task<IActionResult> PostLoginBts([FromBody] AccountBtsLoginDto user)
        {
            var result = await _accountService.LoginLocalAsync(user.UserName, user.Password);

            if (result == null)
            {
                return BadRequest("account not exist");
            }
            var token = _jwtUtils.GenerateToken(result.Id, result.UserName, result.Email);
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("error generate token");
            }
            Response.Headers.Add("token", token);
            return CreatedAtAction(
                nameof(GetAccountById), new { id = result.Id }, result);

        }

        // POST api/Account/register
        [HttpPost("register")]
        [ModelFilter]
        [AccountFilter]
        public async Task<IActionResult> PostRegisterBts([FromBody] AccountBtsRegister user)
        {
            var result = await _accountService.AddRegistedAccountAsync(user);
            return CreatedAtAction(
                nameof(GetAccountById), new { id = result.Id }, result);
        }

        [HttpPut("verify-email")]
        public async Task<IActionResult> VerifyEmailBts([FromBody] string email)
        {
            await _accountService.VerifyEmailAsync(email);
            return NoContent();
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailBts()
        {
            var email = Request.Query["email"].ToString();
            var clientId = Request.Query["clientId"].ToString();
            var code = Request.Query["code"].ToString();
            await _accountService.ConfirmEmailBts(email, clientId, code);
            return Ok();
        }

        // PUT api/Account/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpdateAccount
            (string id, [FromBody] AccountNormalDto user)
        {
            if (id != user.Id)
                return BadRequest();
            await _accountService.UpdateAccountAsync(user);
            return NoContent();
        }

        // DELETE api/Account/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            await _accountService.DeleteAccountAsync(id);
            return NoContent();
        }
    }
}
