using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Data.Specifications;

namespace Bug.Data.Repositories
{
    public interface IEntityRepoBase<T>
    {
        Task<T> GetByIdAsync(string id, CancellationToken cancelltionToken = default);
        Task<T> GetByIdAsync(int id, CancellationToken cancelltionToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancelltionToken = default);
        void Update(T entity);
        void Delete(T entity);
        Task<IReadOnlyList<T>> FindAllAsync(CancellationToken cancelltionToken = default);

        //Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        //IQueryable<T> ApplySpecification(ISpecification<T> spec);
    }
}
