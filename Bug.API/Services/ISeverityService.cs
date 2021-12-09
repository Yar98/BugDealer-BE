using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface ISeverityService
    {
        Task<Severity> GetDetailByIdAsync
            (int id,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Severity>> GetAllSeveritiesAsync
            (CancellationToken cancellationToken = default);
    }
}
