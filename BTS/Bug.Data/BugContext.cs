using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bug.Data
{
    public class BugContext : DbContext
    {
        public BugContext(DbContextOptions options)
            : base(options)
        {

        }

        //public DbSet<Class1> Accounts { get; set; }
    }
}
