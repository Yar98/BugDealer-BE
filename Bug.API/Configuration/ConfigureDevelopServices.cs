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

namespace Bug.API.Configuration
{
    public static class ConfigureDevelopServices
    {
        public static void ConfigureSqlServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BugContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("BugConnection")));
        }
        
    }
}
