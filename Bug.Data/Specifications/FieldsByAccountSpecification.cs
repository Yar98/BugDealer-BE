using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Specifications
{
    public class FieldsByAccountSpecification : BaseSpecification<Field>
    {
        public FieldsByAccountSpecification(string accountId, int customtypeId)
            : base(f=>f.Customtypes.AsQueryable().Where(
                ct=>ct.Id==customtypeId && 
                ct.Accounts.AsQueryable().Where(a=>a.Id==accountId).Any())
            .Any())
        {
            AddInclude(f => f.Customtypes);
            AddInclude("Customtypes.Accounts");
        }
    }
}
