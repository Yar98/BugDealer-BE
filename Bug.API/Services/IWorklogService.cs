using Bug.API.Dto;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface IWorklogService
    {
        Task<Worklog> GetDetailWorklogByIdAsync
            (int id,
            CancellationToken cancellationToken = default);
        Task<Worklog> AddNewWorklogToIssueAsync
            (string issueId,
            WorklogPostDto worklog,
            CancellationToken cancellationToken = default);
        Task DeleteWorklogAsync
           (int id,
           CancellationToken cancellationToken = default);
    }
}
