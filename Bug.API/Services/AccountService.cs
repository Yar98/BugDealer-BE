using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.API.Dto;
using Bug.Data.Infrastructure;
using Bug.API.Utils;
using Bug.Entities.Builder;
using System.Threading;
using Bug.Entities.Model;
using Bug.Data.Specifications;
using Amazon;
using Amazon.CognitoIdentityProvider.Model;

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

        public async Task<string> GenerateTokenGoogleAccountAsync
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
                    .AddUserName(acc.UserName??"")
                    .AddFirstName(acc.GivenName)
                    .AddLastName(acc.SurName)
                    .Build();
                await _unitOfWork.Account.AddAsync(newAccount, cancellationToken);
                _unitOfWork.Save();
                return _jwtUtils.GenerateToken(newAccount.Id, newAccount.UserName, newAccount.Email);
            }
        }

        public async Task<AccountNormalDto> LoginLocalAsync(
            string name, 
            string password,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Account.GetAccountByUserName(name, password);
            if (result != null)
                return new AccountNormalDto
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
            else
                return null;
        }

        public async Task<Account> CheckPermissionsOfRolesOfAccount
            (string accountId,
            int permissionId,
            string projectId,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new AccountByIdCheckRoleSpecification(accountId, permissionId, projectId);
            return await _unitOfWork
                .Account
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
        }

        public async Task<AccountNormalDto> GetAccountByIdAsync
            (string id,
            CancellationToken cancellationToken = default)
        {
            var result =  await _unitOfWork.Account.GetByIdAsync(id, cancellationToken);
            return new AccountNormalDto
            {
                Id = result?.Id,
                UserName = result?.UserName,
                CreatedDate = result?.CreatedDate,
                Email = result?.Email,
                FirstName = result?.FirstName,
                ImageUri = result?.ImageUri,
                Language = result?.LastName,
                LastName = result?.LastName,
                TimezoneId = result?.TimezoneId
            };
        }

        public async Task<Account> GetDetailAccountById
            (string id,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new AccountSpecification(id);
            var result = await _unitOfWork
                .Account
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            return result;
        }

        public async Task<Account> GetDetailAccountByUserNameAsync
            (string username,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new AccountByUserNameSpecification(username);
            var result = await _unitOfWork
                .Account
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            return result;
        }

        public async Task<PaginatedListDto<AccountNormalDto>> GetPaginatedByProjectIdAsync
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            AccountsByProjectSpecification specificationResult =
                new(projectId);
            var result = await _unitOfWork.Account.GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<AccountNormalDto>
            {
                Length = result.Length,
                Items = result.Select(
                    a => new AccountNormalDto
                    {
                        Id = a.Id,
                        UserName = a.UserName,
                        CreatedDate = a.CreatedDate,
                        Email = a.Email,
                        FirstName = a.FirstName,
                        ImageUri = a.ImageUri,
                        Language = a.LastName,
                        LastName = a.LastName,
                        TimezoneId = a.TimezoneId
                    })
                .ToList()
            };
        }

        public async Task<IReadOnlyList<AccountNormalDto>> GetNextByOffsetByProjectIdAsync
            (string projectId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new AccountsByProjectSpecification(projectId);
            var result = await _unitOfWork.Account.GetNextByOffsetNoTrackBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result
                .Select(a => new AccountNormalDto
                {
                    Id = a.Id,
                    UserName = a.UserName,
                    CreatedDate = a.CreatedDate,
                    Email = a.Email,
                    FirstName = a.FirstName,
                    ImageUri = a.ImageUri,
                    Language = a.LastName,
                    LastName = a.LastName,
                    TimezoneId = a.TimezoneId
                })
                .ToList();
        }

        public async Task VerifyEmailAsync
            (string email,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _unitOfWork
                    .Account
                    .AddCognitoUser(email, "Pass@word123", cancellationToken);
            }
            catch(UsernameExistsException)
            {
                await _unitOfWork
                    .Account
                    .ResendVerifyCognito(email, cancellationToken);
            }
            
        }

        public async Task ConfirmEmailBts
            (string email,
            string clientId,
            string code,
            CancellationToken cancellationToken = default)
        {
            await _unitOfWork
                .Account
                .ConfirmSignUp(email, clientId, code, cancellationToken);
            var specificationResult =
                new AccountByEmailSpecification(email);
            var result = await _unitOfWork
                .Account
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            result.UpdateVerifyEmail(true);
            _unitOfWork.Account.Update(result);
            _unitOfWork.Save();
        }

        public async Task<AccountNormalDto> AddRegistedAccountAsync
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
            _unitOfWork.Save();
            return new AccountNormalDto
            {
                Id = result.Id,
                UserName = result.UserName,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email
            };
        }        

        public async Task UpdateAccountAsync
            (AccountNormalDto user,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Account.GetByIdAsync(user.Id, cancellationToken);
            if (user.UserName != null)
                result.UpdateUserName(user.UserName);
            if (user.LastName != null)
                result.UpdateLastName(user.LastName);
            if (user.FirstName != null)
                result.UpdateFirstName(user.FirstName);
            if (user.Email != null)
                result.UpdateEmail(user.Email);
            if (user.Language != null)
                result.UpdateLanguage(user.Language);
            if (user.ImageUri != null)
                result.UpdateImageUri(user.ImageUri);
            _unitOfWork.Account.Update(result);
            _unitOfWork.Save();
        }

        public async Task<int> UpdateAccountWithCheckPasswordAsync
            (AccountPutWithCheckDto user,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new AccountSpecification(user.Id);
            var result = await _unitOfWork
                .Account
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            if (string.Compare(result.Password, user.Password) != 0)
                return 0;
            if (user.UserName != null)
                result.UpdateUserName(user.UserName);
            if (user.LastName != null)
                result.UpdateLastName(user.LastName);
            if (user.FirstName != null)
                result.UpdateFirstName(user.FirstName);
            if (user.Email != null)
                result.UpdateEmail(user.Email);
            if (user.Language != null)
                result.UpdateLanguage(user.Language);
            if (user.ImageUri != null)
                result.UpdateImageUri(user.ImageUri);
            _unitOfWork.Account.Update(result);
            _unitOfWork.Save();
            return 1;
        }

        public async Task DeleteAccountAsync
            (string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Account.GetByIdAsync(id, cancellationToken);
            _unitOfWork.Account.Delete(result);
            _unitOfWork.Save();
        }


    }
}
