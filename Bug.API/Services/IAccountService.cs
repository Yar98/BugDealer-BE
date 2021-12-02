using Bug.API.Dto;
using Bug.Entities.Model;
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
        Task<Account> CheckPermissionsOfRolesOfAccount
            (string accountId,
            int permissionId,
            string projectId,
            CancellationToken cancellationToken = default);
        Task<AccountNormalDto> LoginLocalAsync(string name, string password, CancellationToken cancellationToken = default);
        Task<AccountNormalDto> GetAccountByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<Account> GetDetailAccountById
            (string id,
            CancellationToken cancellationToken = default);
        Task<Account> GetDetailAccountByUserNameAsync
            (string username,
            CancellationToken cancellationToken = default);
        Task<PaginatedListDto<AccountNormalDto>> GetPaginatedByProjectIdAsync
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<AccountNormalDto>> GetNextByOffsetByProjectIdAsync
            (string projectId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default);
        Task VerifyEmailAsync
            (string email,
            CancellationToken cancellationToken = default);
        Task ConfirmEmailBts
            (string email,
            string clientId,
            string code,
            CancellationToken cancellationToken = default);
        Task<AccountNormalDto> AddRegistedAccountAsync(AccountBtsRegister user, CancellationToken cancellationToken = default);
        Task UpdateAccountAsync(AccountNormalDto user, CancellationToken cancellationToken = default);
        Task<int> UpdateAccountWithCheckPasswordAsync
            (AccountPutWithCheckDto user,
            CancellationToken cancellationToken = default);
        Task DeleteAccountAsync(string id, CancellationToken cancellationToken = default);
    }
}
