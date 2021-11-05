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
        Task<string> GenerateTokenAccountGoogle(AccountGoogleLoginDto acc, CancellationToken cancellationToken = default);
        Task<AccountDetailDto> GetAccountByUserName(string name, string password, CancellationToken cancellationToken = default);
        Task<AccountDetailDto> GetAccountById(string id, CancellationToken cancellationToken = default);
        Task<AccountDetailDto> AddRegistedAccount(AccountBtsRegister user, CancellationToken cancellationToken = default);
        Task UpdateAccount(AccountDetailDto user, CancellationToken cancellationToken = default);
        Task DeleteAccount(string id, CancellationToken cancellationToken = default);
    }
}
