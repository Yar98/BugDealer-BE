using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Repositories
{
    public interface IFieldRepo : IEntityRepoBase<Field>
    {
        Task<IReadOnlyList<Field>> GetActiveFieldsByAccountIdAsync(string accountId);
    }
}
