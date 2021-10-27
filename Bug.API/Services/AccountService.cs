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

        public async Task<string> GenerateTokenAccountGoogle(AccountGoogleLoginDto acc)
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
        public async Task<AccountDetailDto> GetAccountByUserName(
            string name, 
            string password)
        {
            var result = await _unitOfWork.Account.GetAccountByUserName(name, password);
            return new AccountDetailDto
            {
                Id = result.Id,
                UserName = result.UserName,
                CreatedDate = result.CreatedDate,
                Email = result.Email,
                FirstName = result.FirstName,
                ImageUri = result.ImageUri,
                Language = result.LastName,
                LastName = result.LastName,
                TimezoneId = result.TimezoneId
            };
        }
        public async Task<AccountDetailDto> GetAccountById(string id)
        {
            var result =  await _unitOfWork.Account.GetByIdAsync(id);
            return new AccountDetailDto
            {
                Id = result.Id,
                UserName = result.UserName,
                CreatedDate = result.CreatedDate,
                Email = result.Email,
                FirstName = result.FirstName,
                ImageUri = result.ImageUri,
                Language = result.LastName,
                LastName = result.LastName,
                TimezoneId = result.TimezoneId
            };
        }
        public async Task<AccountDetailDto> AddRegistedAccount(AccountBtsRegister user)
        {
            var result = new AccountBuilder()
                .AddId(Guid.NewGuid().ToString())
                .AddUserName(user.UserName)
                .AddPassword(user.Password)
                .AddFirstName(user.FirstName)
                .AddLastName(user.LastName)
                .AddEmail(user.Email)
                .Build();
            await _unitOfWork.Account.AddAsync(result);
            return new AccountDetailDto
            {
                Id = result.Id,
                UserName = result.UserName,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email
            };
        }
        public async Task UpdateAccount(AccountDetailDto user)
        {
            var result = new AccountBuilder()
                .AddId(user.Id)
                .AddUserName(user.UserName)
                .AddLastName(user.LastName)
                .AddFirstName(user.FirstName)
                .AddEmail(user.Email)
                .AddLanguage(user.Language)
                .AddImageUri(user.ImageUri)
                .Build();
            await _unitOfWork.Account.UpdateAsync(result);
        }
        public async Task DeleteAccount(string id)
        {
            var result = await _unitOfWork.Account.GetByIdAsync(id);
            await _unitOfWork.Account.DeleteAsync(result);
        }

    }
}
