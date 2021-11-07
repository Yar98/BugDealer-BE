using Bug.API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface IAccountService
    {
        Task<string> GenerateTokenGoogleAccountAsync(AccountGoogleLoginDto acc, CancellationToken cancellationToken = default);
        Task<AccountNormalDto> GetLoginLocalAsync(string name, string password, CancellationToken cancellationToken = default);
        Task<AccountNormalDto> GetAccountByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<PaginatedListDto<AccountNormalDto>> GetPaginatedByProjectAsync
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<AccountNormalDto>> GetNextByOffsetByProjectAsync
            (string projectId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<AccountNormalDto> AddRegistedAccountAsync(AccountBtsRegister user, CancellationToken cancellationToken = default);
        Task UpdateAccountAsync(AccountNormalDto user, CancellationToken cancellationToken = default);
        Task DeleteAccountAsync(string id, CancellationToken cancellationToken = default);
    }
}
