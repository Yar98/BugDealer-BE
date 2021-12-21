using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Core.Utils;
using Bug.Data.Specifications;

namespace Bug.Data.Repositories
{
    public interface IEntityRepoBase<T>
    {
        T GetById(int id);
        Task<T> GetByIdAsync(string id, CancellationToken cancelltionToken = default);
        Task<T> GetByIdAsync(int id, CancellationToken cancelltionToken = default);
        Task<T> GetByIdAsync(object[] id, CancellationToken cancelltionToken = default);
        Task<T> GetEntityBySpecAsync
            (ISpecification<T> specificationResult,
            CancellationToken cancelltionToken = default);
        Task<IReadOnlyList<T>> GetAllEntitiesNoTrackBySpecAsync
            (ISpecification<T> specificationResult,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAllEntitiesBySpecAsync
            (ISpecification<T> specificationResult,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<PaginatedList<T>> GetPaginatedNoTrackBySpecAsync
            (int pageIndex,
            int pageSize,
            string sortOrder,
            ISpecification<T> specificationResult,
            CancellationToken cancelltionToken = default);
        Task<PaginatedList<T>> GetPaginatedBySpecAsync
            (int pageIndex,
            int pageSize,
            string sortOrder,
            ISpecification<T> specificationResult,
            CancellationToken cancelltionToken = default);
        Task<IReadOnlyList<T>> GetNextByOffsetNoTrackBySpecAsync
            (int offset,
            int next,
            string sortOrder,
            ISpecification<T> specificationResult,
            CancellationToken cancelltionToken = default);
        Task<IReadOnlyList<T>> GetNextByOffsetBySpecAsync
            (int offset,
            int next,
            string sortOrder,
            ISpecification<T> specificationResult,
            CancellationToken cancelltionToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancelltionToken = default);
        void Add(T entity);
        void Update(T entity);
        void Unchange(T entity);
        void Attach(T entity);
        void Detach(T entity);
        void Delete(T entity);
        Task<IReadOnlyList<T>> FindAllAsync(CancellationToken cancelltionToken = default);

        //Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        //IQueryable<T> ApplySpecification(ISpecification<T> spec);
    }
}
