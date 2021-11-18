using Bug.Data.Infrastructure;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PriorityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Priority> GetPriorityByIdAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork
                .Priority
                .GetByIdAsync(id, cancellationToken);
        }

        public async Task<IReadOnlyList<Priority>> GetPrioritiesAsync
            (CancellationToken cancellationToken = default)
        {
            return await _unitOfWork
                .Priority
                .FindAllAsync(cancellationToken);
        }
    }
}
