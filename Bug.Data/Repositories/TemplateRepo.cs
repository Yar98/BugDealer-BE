using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Repositories
{
    public class TemplateRepo : EntityRepoBase<Template>, ITemplateRepo
    {
        public TemplateRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {
            
        }
        public override IQueryable<Template> SortOrder
            (IQueryable<Template> result, string sortOrder)
        {
            throw new NotImplementedException();
        }
    }
}
