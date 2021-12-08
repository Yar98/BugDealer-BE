using Bug.Core.Utils;
using Bug.Data.Extensions;
using Bug.Data.Specifications;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;


namespace Bug.Data.Repositories
{
    public class AccountRepo : EntityRepoBase<Account>, IAccountRepo
    {
        private readonly IConfiguration _config;
        public AccountRepo(BugContext repositoryContext, IConfiguration config)
            : base(repositoryContext)
        {
            _config = config;
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            return await _bugContext
                .Accounts
                .FirstOrDefaultAsync(a=>a.Email.Equals(email));
        }

        public async Task<Account> GetAccountByUserName
            (string userName, 
            string password)
        {
            return await _bugContext
                .Accounts
                .FirstOrDefaultAsync(
                a => a.UserName == userName && 
                a.Password == password);
        }

        public async Task AddCognitoUser
            (string email, 
            string pass,
            CancellationToken cancellationToken = default)
        {
            var provider = new AmazonCognitoIdentityProviderClient(
                _config.GetSection("Cognito")["AccessKeyId"],
                _config.GetSection("Cognito")["AccessSecretKey"],
                RegionEndpoint.GetBySystemName(_config.GetSection("Cognito")["Region"]));
            var signUpRequest = new SignUpRequest
            {
                ClientId = _config.GetSection("Cognito").GetSection("ClientId").Value,
                Username = email,
                Password = pass
            };
            signUpRequest.UserAttributes.Add(new AttributeType
            {
                Name = "email",
                Value = email
            });
            await provider.SignUpAsync(signUpRequest, cancellationToken);            
        }

        public async Task ResendVerifyCognito
            (string email,
            CancellationToken cancellationToken = default)
        {
            var provider = new AmazonCognitoIdentityProviderClient(
                _config.GetSection("Cognito")["AccessKeyId"],
                _config.GetSection("Cognito")["AccessSecretKey"],
                RegionEndpoint.GetBySystemName(_config.GetSection("Cognito")["Region"]));
            var resendVerifyRequest = new ResendConfirmationCodeRequest
            {
                ClientId = _config.GetSection("Cognito").GetSection("ClientId").Value,
                Username = email,
            };
            await provider.ResendConfirmationCodeAsync(resendVerifyRequest, cancellationToken);
        }

        public async Task ConfirmSignUp
            (string email,
            string clientId,
            string code,
            CancellationToken cancellationToken = default)
        {
            var provider = new AmazonCognitoIdentityProviderClient(
                _config.GetSection("Cognito")["AccessKeyId"],
                _config.GetSection("Cognito")["AccessSecretKey"],
                RegionEndpoint.GetBySystemName(_config.GetSection("Cognito")["Region"]));
            var confirmSignUpRequest = new ConfirmSignUpRequest
            {
                ClientId = clientId,
                Username = email,
                ConfirmationCode = code
            };
            await provider.ConfirmSignUpAsync(confirmSignUpRequest, cancellationToken);
        }

        public override IQueryable<Account> SortOrder
            (IQueryable<Account> result,
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "name":
                    //result = result.OrderBy(p => p.Name);
                    break;
                case "startdate":
                    //result = result.OrderBy(p => p.StartDate);
                    break;
                case "startdate_desc":
                    //result = result.OrderByDescending(p => p.StartDate);
                    break;
                case "enddate":
                    //result = result.OrderBy(p => p.EndDate);
                    break;
                case "enddate_desc":
                    //result = result.OrderByDescending(p => p.EndDate);
                    break;
                case "recentdate":
                    //result = result.OrderBy(p => p.RecentDate);
                    break;
                case "recentdate_desc":
                    //result = result.OrderByDescending(p => p.RecentDate);
                    break;
                default:
                    result = result.OrderBy(p => p.Id);
                    break;
            }
            return result;
        }

    }
}
