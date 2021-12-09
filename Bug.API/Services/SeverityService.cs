using Bug.Data.Infrastructure;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public class SeverityService : ISeverityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SeverityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Severity> GetDetailByIdAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork
                .Severity
                .GetByIdAsync(id, cancellationToken);
        }

        public async Task<IReadOnlyList<Severity>> GetAllSeveritiesAsync
            (CancellationToken cancellationToken = default)
        {
            return await _unitOfWork
                .Severity
                .FindAllAsync(cancellationToken);
        }
    }
}
