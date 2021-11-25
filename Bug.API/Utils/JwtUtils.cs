using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bug.API.Dto;

namespace Bug.API.Utils
{
    public class JwtUtils : IJwtUtils
    {
        private readonly string Secret = "e00e1ae681d9ad7984bd79009672c387178ac8420bcb3a9e769c01e3dce4a1b2";
        //private readonly string Issuer = "https://bts.com";
        //private readonly string Audience = "https://bts.com";

        public string GenerateToken(string id, string name, string email)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim("id", id),
                    new Claim("name", name),
                    new Claim("email", email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                //Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public AccountBtsJwtDto ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    //ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //var testId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                // return user id from JWT token if validation successful
                return new AccountBtsJwtDto
                {
                    Id = jwtToken.Claims.First(x => x.Type == "id").Value,
                    Name = jwtToken.Claims.First(x => x.Type == "name").Value,
                    Email = jwtToken.Claims.First(x => x.Type == "email").Value
                };
            }
            catch (Exception)
            {
                // return null if validation fails
                return null;
            }
        }



    }
}
