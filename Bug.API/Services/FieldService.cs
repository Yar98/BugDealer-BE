using Bug.Data.Infrastructure;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bug.Data.Specifications;
using Bug.API.Dto;

namespace Bug.API.Services
{
    public class FieldService : IFieldService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FieldService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Field> GetFieldByIdAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork
                .Field
                .GetByIdAsync(id, cancellationToken);
        }

        public async Task<IReadOnlyList<Field>> GetAllFieldsAsync
            (CancellationToken cancellationToken = default)
        {
            return await _unitOfWork
                .Field
                .FindAllAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<FieldNormalDto>> GetFieldsByAccountCustomtypeAsync
            (string accountId,
            int customtypeId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new FieldsByAccountSpecification(accountId, customtypeId);
            var activeFields = await _unitOfWork
                .Field
                .GetAllEntitiesNoTrackBySpecAsync(specificationResult, sortOrder, cancellationToken);
            if (activeFields == null) return null;
            var fields = await _unitOfWork
                .Field
                .FindAllAsync(cancellationToken);
            return fields
                .Select(f => new FieldNormalDto
                {
                    Id = f.Id,
                    Active = activeFields.Contains(f),
                    Description = f.Description,
                    Name = f.Name
                })
                .ToList();
        }


    }
}
