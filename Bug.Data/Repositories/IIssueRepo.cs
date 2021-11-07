﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Core.Utils;
using Bug.Data.Specifications;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public interface IIssueRepo : IEntityRepoBase<Issue>
    {
        Task<Issue> GetIssuelAsync
            (ISpecification<Issue> specificationResult,
            CancellationToken cancelltionToken = default);
    }
}
