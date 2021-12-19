using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.Data.Repositories
{
    public interface IAccountProjectRoleRepo : IEntityRepoBase<AccountProjectRole>
    {
        void UpdateMultiByRoleIdProjectId(string projectId, List<Role> roles, int? roleId);
        int UpdateAprBeforeDeleteRole(int roleId);
        Task DeleteMemberFromProjectAsync
            (string projectId, 
            string accountId,
            CancellationToken cancellationToken = default);
    }
}
