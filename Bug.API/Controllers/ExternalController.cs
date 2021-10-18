using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Controllers
{
    [ApiController]
    [AllowAnonymous,Route("api/[controller]")]
    public class ExternalController : Controller
    {
        [HttpGet("signinexternal")]
        public IActionResult SigninExternal(string provide, string returnUrl)
        {
            var properties = new AuthenticationProperties { 
                RedirectUri = Url.Action("SigninExternalCallback") };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("signinexternalcallback")]
        public async Task<IActionResult> SigninExternalCallback(
            string returnUrl = null,
            string remoteNull = null)
        {
            var result = await HttpContext.AuthenticateAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            return Json(claims);
        }
    }
}
