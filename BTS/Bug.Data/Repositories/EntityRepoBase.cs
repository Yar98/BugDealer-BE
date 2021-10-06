using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Repositories
{
    public abstract class EntityRepoBase<T> : IEntityRepoBase<T> where T : class
    {
        protected RepoContext RepoContext { get; set; }
        public EntityRepoBase(RepoContext repoContext)
        {
            this.RepoContext = repoContext;
        }
        public void Create(T entity)
        {
            this.RepoContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this.RepoContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return this.RepoContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepoContext.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            this.RepoContext.Set<T>().Update(entity);
        }
    }
}
