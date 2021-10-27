using Bug.API.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface IAccountService
    {
        Task<string> GenerateTokenAccountGoogle(AccountGoogleLoginDto acc);
        Task<AccountDetailDto> GetAccountByUserName(string name, string password);
        Task<AccountDetailDto> GetAccountById(string id);
        Task<AccountDetailDto> AddRegistedAccount(AccountBtsRegister user);
        Task UpdateAccount(AccountDetailDto user);
        Task DeleteAccount(string id);
    }
}
