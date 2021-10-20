using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Core.Specifications;

namespace Bug.Data.Repositories
{
    public interface IEntityRepoBase<T>
    {
        Task<IReadOnlyList<T>> FindAll(CancellationToken cancelltionToken = default);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancelltionToken = default);
        Task UpdateAsync(T entity, CancellationToken cancelltionToken = default);
        Task DeleteAsync(T entity, CancellationToken cancelltionToken = default);
        IQueryable<T> ApplySpecification(ISpecification<T> spec);
    }
}
