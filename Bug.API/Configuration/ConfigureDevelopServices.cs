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

namespace Bug.API.Configuration
{
    public static class ConfigureDevelopServices
    {
        public static void ConfigureSqlServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BugContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("BugConnection")));
                //c.UseSqlServer(configuration.GetConnectionString("DockerConnection")));
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
    }
}
