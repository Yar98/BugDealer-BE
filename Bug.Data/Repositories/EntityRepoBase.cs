using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Data.Specifications;
using Bug.Entities.Model;
using Bug.Core.Utils;
using Bug.Data.Extensions;

namespace Bug.Data.Repositories
{
    public abstract class EntityRepoBase<T> : IEntityRepoBase<T> where T : IEntityBase
    {
        protected readonly BugContext _bugContext;
        public EntityRepoBase(BugContext repoContext)
        {
            _bugContext = repoContext;
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancelltionToken = default)
        {
            var keyValues = new object[] { id };
            return await _bugContext.Set<T>().FindAsync(keyValues, cancelltionToken);
        }

        public async Task<T> GetByIdAsync(string id, CancellationToken cancelltionToken = default)
        {
            var keyValues = new object[] { id };
            return await _bugContext.Set<T>().FindAsync(keyValues, cancelltionToken);
        }

        public async Task<T> GetByIdAsync(object[] id, CancellationToken cancelltionToken = default)
        {            
            return await _bugContext.Set<T>().FindAsync(id, cancelltionToken);
        }

        public async Task<T> GetEntityBySpecAsync
            (ISpecification<T> specificationResult,
            CancellationToken cancelltionToken = default)
        {
            return await _bugContext
                .Set<T>()
                .Specify(specificationResult)
                .FirstOrDefaultAsync(cancelltionToken);
        }

        public async Task<IReadOnlyList<T>> GetAllEntitiesNoTrackBySpecAsync
            (ISpecification<T> specificationResult,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var result = _bugContext
                .Set<T>()
                .Specify(specificationResult);
            var tem = result.ToQueryString();
            result = SortOrder(result, sortOrder);            
            if (result == null)
                return null;
            return await result.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllEntitiesBySpecAsync
            (ISpecification<T> specificationResult,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var result = _bugContext
                .Set<T>()
                .Specify(specificationResult);
            result = SortOrder(result, sortOrder);
            if (result == null)
                return null;
            return await result.ToListAsync(cancellationToken);
        }

        public async Task<PaginatedList<T>> GetPaginatedNoTrackBySpecAsync
            (int pageIndex,
            int pageSize,
            string sortOrder,
            ISpecification<T> specificationResult,
            CancellationToken cancelltionToken = default)
        {
            var result = _bugContext
                .Set<T>()
                .Specify(specificationResult);
            result = SortOrder(result, sortOrder);
            return await PaginatedList<T>
                .CreateListAsync(result.AsNoTracking(), pageIndex, pageSize, cancelltionToken);
        }

        public async Task<PaginatedList<T>> GetPaginatedBySpecAsync
            (int pageIndex,
            int pageSize,
            string sortOrder,
            ISpecification<T> specificationResult,
            CancellationToken cancelltionToken = default)
        {
            var result = _bugContext
                .Set<T>()
                .Specify(specificationResult);
            result = SortOrder(result, sortOrder);
            return await PaginatedList<T>
                .CreateListAsync(result, pageIndex, pageSize, cancelltionToken);
        }

        public async Task<IReadOnlyList<T>> GetNextByOffsetNoTrackBySpecAsync
            (int offset,
            int next,
            string sortOrder,
            ISpecification<T> specificationResult,
            CancellationToken cancelltionToken = default)
        {
            var result = _bugContext
                .Set<T>()
                .Specify(specificationResult);
            result = SortOrder(result, sortOrder);
            return await result
                .Skip(offset)
                .Take(next)
                .AsNoTracking()
                .ToListAsync(cancelltionToken);
        }

        public async Task<IReadOnlyList<T>> GetNextByOffsetBySpecAsync
            (int offset,
            int next,
            string sortOrder,
            ISpecification<T> specificationResult,
            CancellationToken cancelltionToken = default)
        {
            var result = _bugContext
                .Set<T>()
                .Specify(specificationResult);
            result = SortOrder(result, sortOrder);
            return await result
                .Skip(offset)
                .Take(next)
                .ToListAsync(cancelltionToken);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancelltionToken = default)
        {
            await _bugContext.Set<T>().AddAsync(entity, cancelltionToken);
            //await _bugContext.SaveChangesAsync(cancelltionToken);
            return entity;
        }

        public async Task<IReadOnlyList<T>> FindAllAsync(CancellationToken cancelltionToken = default)
        {
            return await _bugContext.Set<T>().ToListAsync(cancelltionToken);
        }

        public void Add(T entity)
        {
            _bugContext.Entry(entity).State = EntityState.Added;
            //await _bugContext.SaveChangesAsync(cancelltionToken);
        }

        public void Update(T entity)
        {
            _bugContext.Entry(entity).State = EntityState.Modified;
        }

        public void Attach(T entity)
        {
            _bugContext.Attach(entity);
        }

        public void Detach(T entity)
        {
            _bugContext.Entry(entity).State = EntityState.Detached;
        }

        public void Delete(T entity)
        {
            _bugContext.Set<T>().Remove(entity);
            //await _bugContext.SaveChangesAsync(cancelltionToken);
        }

        public abstract IQueryable<T> SortOrder
            (IQueryable<T> result,
            string sortOrder);


        /*
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
            return secondaryResult.Where(spec.Criteria);
        }   
        */



    }


}
