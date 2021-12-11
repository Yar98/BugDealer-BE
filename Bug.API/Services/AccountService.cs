using Bug.API.Dto;
using Bug.Data.Infrastructure;
using Bug.API.Utils;
using Bug.Entities.Builder;
using Bug.Entities.Model;
using Bug.Data.Specifications;
using Bug.API.BtsException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Amazon;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace Bug.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtUtils _jwtUtils;
        private readonly IConfiguration _config;
        private readonly string htmlBody = @"<html>
<head></head>
<body>
  <h1>Cap Bai Trung hay vl</h1>
  <p>{0} invite you to join his/her project. Click
    <a href='http://localhost:4200/projects/invitation-info?project={1}'>HERE</a> to xem Cap Bai Trung.</p>
</body>
</html>";

        public AccountService(IUnitOfWork unitOfWork, IJwtUtils jwtUtils, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _jwtUtils = jwtUtils;
            _config = config;
        }

        public async Task SendInviteEmail
            (string fromEmail,
            string toEmail, 
            string code,
            CancellationToken cancellationToken = default)
        {            
            using var client =
                new AmazonSimpleEmailServiceClient(_config.GetSection("Cognito")["AccessKeyId"], _config.GetSection("Cognito")["AccessSecretKey"], RegionEndpoint.GetBySystemName(_config.GetSection("Cognito")["Region"]));
            var sendRequest = new SendEmailRequest
            {
                Source = "tienfu97@gmail.com",
                Destination = new Destination
                {
                    ToAddresses = new List<string> { toEmail }
                },
                Message = new Message
                {
                    Subject = new Content("Xem cap bai trung di"),
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Charset = "UTF-8",
                            Data = string.Format(htmlBody, fromEmail, code)
                        },
                        Text = new Content
                        {
                            Charset = "UTF-8",
                            Data = fromEmail + " invite you to join his/her project." +
                            "Click http://localhost:4200/projects/invitation-info?project=" +
                            code + "to join."
                        }
                    }
                }
            };
            await client.SendEmailAsync(sendRequest, cancellationToken);
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

        public async Task<IReadOnlyList<Account>> GetAllByProjectIdAsync
            (string projectId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new AccountsByProjectIdSpecification(projectId);
            return await _unitOfWork
                .Account
                .GetAllEntitiesBySpecAsync(specificationResult, sortOrder, cancellationToken);
        }

        public async Task<PaginatedListDto<AccountNormalDto>> GetPaginatedByProjectIdSearch
            (string projectId,
            string search,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new AccountsByProjectIdSearchSpecification(projectId, search);
            var result = await _unitOfWork
                .Account
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);

            return new PaginatedListDto<AccountNormalDto>
            {
                Length = result.Length,
                Items = result.Select(a => new AccountNormalDto
                {
                    CreatedDate = a.CreatedDate,
                    Email = a.Email,
                    FirstName = a.FirstName,
                    Id = a.Id,
                    ImageUri = a.ImageUri,
                    Language = a.Language,
                    LastName = a.LastName,
                    TimezoneId = a.TimezoneId,
                    UserName = a.UserName,
                    VerifyEmail = a.VerifyEmail 
                }).ToList()
            };
        }

        public async Task<PaginatedListDto<Account>> GetPaginatedByProjectIdAsync
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new AccountsByProjectIdSpecification(projectId);
            var result = await _unitOfWork
                .Account
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Account>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Account>> GetNextByOffsetByProjectIdAsync
            (string projectId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new AccountsByProjectIdSpecification(projectId);
            var result = await _unitOfWork.Account.GetNextByOffsetNoTrackBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
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

        public async Task ForgotPassword
            (string email,
            CancellationToken cancellationToken = default)
        {
            await _unitOfWork
               .Account
               .ForgotPassword(email, cancellationToken);
        }

        public async Task ConfirmForgotPassword
            (ForgotPasswordDto item,
            CancellationToken cancellationToken = default)
        {
            await _unitOfWork
                .Account
                .ConfirmForgotPassword(item.Email, item.Code, cancellationToken);
            var specificationResult =
                new AccountByEmailSpecification(item.Email);
            var result = await _unitOfWork
                .Account
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            if (result == null)
                return;
            result.UpdatePassword(item.NewPassword);
            _unitOfWork.Account.Update(result);

            _unitOfWork.Save();
        }

        public async Task ConfirmEmailBts
            (string email,
            string clientId,
            string code,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new AccountByEmailSpecification(email);
            var result = await _unitOfWork
                .Account
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            if (result == null)
                return;
            await _unitOfWork
                .Account
                .ConfirmSignUp(email, clientId, code, cancellationToken);

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
                .AddLanguage(user.Language)
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
            var result = await _unitOfWork
                .Account
                .GetByIdAsync(user.Id, cancellationToken);
            if (user.Language != null)
                result.UpdateLanguage(user.Language);
            if (user.ImageUri != null)
                result.UpdateImageUri(user.ImageUri);
            if (user.TimezoneId != null)
                result.UpdateTimezoneId(user.TimezoneId);
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
            if (string.Compare(result.Password, user.OldPassWord) != 0 &&
                !string.IsNullOrEmpty(result.Password) && 
                !string.IsNullOrEmpty(user.OldPassWord))
                throw new OldPasswordWrong("Old password is false");
            if (user.UserName != null)
                result.UpdateUserName(user.UserName);
            if (user.LastName != null)
                result.UpdateLastName(user.LastName);
            if (user.FirstName != null)
                result.UpdateFirstName(user.FirstName);
            if (user.Email != null)
            {
                await _unitOfWork
                    .Account
                    .DeleteEmailAfterChangeEmail(result.Email, cancellationToken);
                result.UpdateEmail(user.Email);
                result.UpdateVerifyEmail(false);
            }
            if (user.NewPassword != null)
                result.UpdatePassword(user.NewPassword);
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
