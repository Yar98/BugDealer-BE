using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.API.Utils;
using Bug.API.Dto;
using Bug.API.Services;

namespace Bug.API.Controllers
{
    [ApiController]
    [AllowAnonymous,Route("api/[controller]")]
    public class ExternalController : Controller
    {
        private readonly IAccountService _accountService;
        public ExternalController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("signinexternal")]
        public IActionResult SigninExternal(string provide, string returnUrl)
        {
            var properties = new AuthenticationProperties { 
                RedirectUri = Url.Action("SigninExternalCallback",new { returnUrl }) };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("signinexternalcallback")]
        public async Task<IActionResult> SigninExternalCallback(
            string returnUrl = null,
            string remoteNull = null)
        {
            var result = await HttpContext.AuthenticateAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            
            if (result.Succeeded)
            {
                var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
                var user = new AccountGoogleLoginDto
                {
                    GoogleId = claims.FirstOrDefault(c => c.Type.Contains("/nameidentifier")).Value,
                    Email = claims.FirstOrDefault(c => c.Type.Contains("/emailaddress")).Value,
                    GivenName = claims.FirstOrDefault(c => c.Type.Contains("/givenname"))?.Value,
                    SurName = claims.FirstOrDefault(c => c.Type.Contains("/surname"))?.Value
                };
                var token = await _accountService.GenerateTokenGoogleAccountAsync(user);
                Response.Headers.Add("token", token);
                //return StatusCode(200, Json(claims));
                return string.IsNullOrEmpty(token)?
                    BadRequest("Error in creating token for google account") :
                    StatusCode(200, token);
            }
            else
            {
                return BadRequest(result.Failure);
            }

            //return Json(claims);
            //return StatusCode(200);
        }
    }
}
