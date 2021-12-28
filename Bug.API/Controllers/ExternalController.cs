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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Bug.Infrastructure.Services;
using Bug.Core.Common;

namespace Bug.API.Controllers
{
    [ApiController]
    [AllowAnonymous,Route("api/[controller]")]
    public class ExternalController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _config;
        private readonly ILogger<ExternalController> _logger;
        public ExternalController(IAccountService accountService, ILogger<ExternalController> logger, IConfiguration config)
        {
            _accountService = accountService;
            _logger = logger;
            _config = config;
        }

        [HttpGet("signinexternal")]
        public IActionResult SigninExternal(string provide, string returnUrl)
        {
            _logger.LogInformation(returnUrl);
            var properties = new AuthenticationProperties { 
                RedirectUri = Url.Action("SigninExternalCallback",new { returnUrl }) };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("signinexternalcallback")]
        public async Task<IActionResult> SigninExternalCallback(
            string returnUrl = null,
            string remoteNull = null)
        {
            _logger.LogInformation("CALLBACK: " + returnUrl);
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
                
                return new RedirectResult($"{returnUrl}?token={token}");
            }
            else
            {
                return BadRequest(returnUrl);
            }
        }

        [HttpGet("upload")]
        public async Task<IActionResult> GetUploadLink()
        {
            var result = new AmazonS3Bts(_config)
                .GenerateUploadPreSignedURL("trashzip", "hello");
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("download/attachment/{attachmentId}")]
        public async Task<IActionResult> GetDownloadLink(string attachmentId)
        {
            var result = new AmazonS3Bts(_config)
                .GenerateDownloadPreSignedURL("bugdealer", attachmentId);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("download/guideline")]
        public async Task<IActionResult> GetGuideDownloadLink()
        {
            var result = new
            {
                url = new AmazonS3Bts(_config)
                .GenerateDownloadPreSignedURL("bugdealer", "Guideline.docx") 
            };
            return Ok(Bts.ConvertJson(result));
        }
    }
}
