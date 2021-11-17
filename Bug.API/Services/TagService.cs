using Bug.Data.Infrastructure;
using Bug.Data.Specifications;
using Bug.Entities.Model;
using Bug.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Tag> GetTagByIdAsync
            (int id, 
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Tag.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Tag> GetDetailTagByIdAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new TagSpecification(id);
            return await _unitOfWork
                .Tag
                .GetEntityAsync(specificationResult, cancellationToken);
        }

        public async Task<IReadOnlyList<Tag>> GetTagsByCategoryIdAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new TagsByCategoryIdSpecification(id);
            return await _unitOfWork
                .Tag
                .GetAllEntitiesAsync(specificationResult, cancellationToken);
        }


    }
}
