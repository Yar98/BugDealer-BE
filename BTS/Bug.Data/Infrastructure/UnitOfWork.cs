﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Infrastructure
{
    class UnitOfWork : IUnitOfWork
    {
        private RepoContext _repoContext;
    }
}
