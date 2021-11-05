using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.API.Dto;
using Bug.Data.Infrastructure;
using Bug.API.Utils;
using Bug.Entities.Builder;
using System.Threading;

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

        public async Task<string> GenerateTokenAccountGoogle
            (AccountGoogleLoginDto acc,
            CancellationToken cancellationToken = default)
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
                await _unitOfWork.Account.AddAsync(newAccount, cancellationToken);
                return _jwtUtils.GenerateToken(newAccount.Id, newAccount.UserName, newAccount.Email);
            }
        }
        public async Task<AccountDetailDto> GetAccountByUserName(
            string name, 
            string password,
            CancellationToken cancellationToken = default)
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
        public async Task<AccountDetailDto> GetAccountById
            (string id,
            CancellationToken cancellationToken = default)
        {
            var result =  await _unitOfWork.Account.GetByIdAsync(id, cancellationToken);
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
        public async Task<AccountDetailDto> AddRegistedAccount
            (AccountBtsRegister user,
            CancellationToken cancellationToken = default)
        {
            var result = new AccountBuilder()
                .AddId(Guid.NewGuid().ToString())
                .AddUserName(user.UserName)
                .AddPassword(user.Password)
                .AddFirstName(user.FirstName)
                .AddLastName(user.LastName)
                .AddEmail(user.Email)
                .Build();
            await _unitOfWork.Account.AddAsync(result, cancellationToken);
            return new AccountDetailDto
            {
                Id = result.Id,
                UserName = result.UserName,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email
            };
        }
        public async Task UpdateAccount
            (AccountDetailDto user,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Account.GetByIdAsync(user.Id, cancellationToken);
            result.UpdateUserName(user.UserName);
            result.UpdateLastName(user.LastName);
            result.UpdateFirstName(user.FirstName);
            result.UpdateEmail(user.Email);
            result.UpdateLanguage(user.Language);
            result.UpdateImageUri(user.ImageUri);
            await _unitOfWork.Account.UpdateAsync(result,cancellationToken);
        }
        public async Task DeleteAccount
            (string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Account.GetByIdAsync(id, cancellationToken);
            await _unitOfWork.Account.DeleteAsync(result, cancellationToken);
        }

    }
}
