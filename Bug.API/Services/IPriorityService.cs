using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface IPriorityService
    {
        Task<Priority> GetPriorityByIdAsync
            (int id,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Priority>> GetPrioritiesAsync
            (CancellationToken cancellationToken = default);
    }
}
