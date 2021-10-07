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
        protected BugContext BugContext { get; set; }
        public EntityRepoBase(BugContext repoContext)
        {
            this.BugContext = repoContext;
        }
        public void Create(T entity)
        {
            this.BugContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this.BugContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return this.BugContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.BugContext.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            this.BugContext.Set<T>().Update(entity);
        }
    }
}
