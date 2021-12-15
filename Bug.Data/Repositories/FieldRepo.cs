using Bug.Entities.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Repositories
{
    public class FieldRepo : EntityRepoBase<Field>, IFieldRepo
    {
        public FieldRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<IReadOnlyList<Field>> GetActiveFieldsByAccountIdAsync(string accountId)
        {
            var account = new SqlParameter("account", accountId);
            return await _bugContext
                .Fields
                .FromSqlRaw("EXECUTE dbo.GetActiveFieldsByAccountId @account", account)
                .ToListAsync();
        }

        public override IQueryable<Field> SortOrder
            (IQueryable<Field> result, string sortOrder)
        {
            return null;
        }
    }
}
