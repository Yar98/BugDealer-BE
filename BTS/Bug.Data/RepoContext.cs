using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bug.Data
{
    public class RepoContext : DbContext
    {
        public RepoContext(DbContextOptions options)
            : base(options)
        {

        }

        //public DbSet<Class1> Accounts { get; set; }
    }
}
