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
using Bug.Core.Utility;
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

        public async Task<T> AddAsync(T entity, CancellationToken cancelltionToken = default)
        {
            await _bugContext.Set<T>().AddAsync(entity, cancelltionToken);
            //await _bugContext.SaveChangesAsync(cancelltionToken);
            return entity;
        }

        public void Delete(T entity)
        {
            _bugContext.Set<T>().Remove(entity);
            //await _bugContext.SaveChangesAsync(cancelltionToken);
        }

        public async Task<IReadOnlyList<T>> FindAllAsync(CancellationToken cancelltionToken = default)
        {
            return await _bugContext.Set<T>().ToListAsync(cancelltionToken);
        }

        public void Update(T entity)
        {
            _bugContext.Entry(entity).State = EntityState.Modified;
            //await _bugContext.SaveChangesAsync(cancelltionToken);
        }


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
