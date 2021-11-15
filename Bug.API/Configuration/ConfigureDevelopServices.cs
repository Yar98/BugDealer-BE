using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Bug.API.ActionFilter;

namespace Bug.API.Configuration
{
    public static class ConfigureDevelopServices
    {
        public static void ConfigureSqlServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BugContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("BugConnection")));
                //c.UseSqlServer(configuration.GetConnectionString("DockerConnection")));
                // c.UseSqlServer(configuration.GetConnectionString("MacConnection")));
        }
        public static void ConfigureGoogleServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/api/external/signinexternal";
                })
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                        configuration.GetSection("GoogleCredentials");
                    //options.CallbackPath = "/api/external/signinexternal";
                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });
        }
        public static void ConfigureJwtServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters();
                });

        }
    }
}
