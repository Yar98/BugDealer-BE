using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BugContext _bugContext;
        
        public UnitOfWork(BugContext bugContext)
        {
            _bugContext = bugContext;
        }

        public void Save()
        {
            
        }
    }
}
