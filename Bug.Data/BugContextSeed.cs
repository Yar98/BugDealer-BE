using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data
{
    public class BugContextSeed
    {
        public static async Task SeedAsync(BugContext bugContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            try
            {
                if(!await bugContext.Projects.AnyAsync())
                {
                    await bugContext.Projects.AddRangeAsync(
                        GetPreconfiguredProjects());
                    await bugContext.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var log = loggerFactory.CreateLogger<BugContextSeed>();
                log.LogError(ex.Message);
                throw;
            }
        }
        // mỗi hàm này là 1 table 
        static IEnumerable<Project> GetPreconfiguredProjects()
        {
            return new List<Project>()
            {
                //fill data trong đây 
                //chú ý datetime 
                //new Project(,,,,),
                //new Project(,,,,)
            };
        }
    }
    
}
