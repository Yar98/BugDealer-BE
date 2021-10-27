using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.API.Services.DTO;
using Bug.Data.Infrastructure;
using Bug.API.Utils;
using Bug.Entities.Builder;

namespace Bug.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtUtils _jwtUtils;
        public AccountService(IUnitOfWork unitOfWork, IJwtUtils jwtUtils)
        {
            _unitOfWork = unitOfWork;
            _jwtUtils = jwtUtils;
        }

        public async Task<string> GenerateTokenAccountGoogle(AccountGoogleDto acc)
        {
            var result = await _unitOfWork.Account.GetAccountByEmail(acc.Email);
            if (result != null) // exist account
            {
                return _jwtUtils.GenerateToken(result.Id, result.UserName, result.Email);
            }
            else // register new account 
            {
                var newAccount = new AccountBuilder()
                    .AddId(Guid.NewGuid().ToString())
                    .AddEmail(acc.Email)
                    .AddUserName(acc.UserName)
                    .AddFirstName(acc.GivenName)
                    .AddLastName(acc.SurName)
                    .Build();
                await _unitOfWork.Account.AddAsync(newAccount);
                return _jwtUtils.GenerateToken(newAccount.Id, newAccount.UserName, newAccount.Email);
            }
        }


    }
}
