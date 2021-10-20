using Microsoft.EntityFrameworkCore;
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
    public abstract class EntityRepoBase<T> : IEntityRepoBase<T> where T : class
    {
        protected readonly BugContext _bugContext;
        public EntityRepoBase(BugContext repoContext)
        {
            _bugContext = repoContext;
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancelltionToken = default)
        {
            await _bugContext.Set<T>().AddAsync(entity);
            await _bugContext.SaveChangesAsync(cancelltionToken);
            return entity;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancelltionToken = default)
        {
            _bugContext.Set<T>().Remove(entity);
            await _bugContext.SaveChangesAsync(cancelltionToken);
        }

        public async Task<IReadOnlyList<T>> FindAll(CancellationToken cancelltionToken = default)
        {
            return await _bugContext.Set<T>().ToListAsync(cancelltionToken);
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancelltionToken = default)
        {
            _bugContext.Entry(entity).State = EntityState.Modified;
            await _bugContext.SaveChangesAsync(cancelltionToken);
        }

        public IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_bugContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult
                            .Where(spec.Criteria);
        }       
    }
}
