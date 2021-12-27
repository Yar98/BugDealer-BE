using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Core.Utils;
using Bug.Data.Specifications;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public interface IAccountRepo : IEntityRepoBase<Account>
    {
        Task DeleteCognitoUser
            (string email,
            CancellationToken cancellationToken = default);
        Task<Account> GetAccountByEmail(string email);
        Task<Account> GetAccountByUserNamePassword(string userName, string password);
        Task<Account> GetAccountByEmailPassword
            (string email,
            string password);
        Task AddCognitoUser
            (string email,
            string pass,
            CancellationToken cancellationToken = default);
        Task ResendVerifyCognito
            (string email,
            CancellationToken cancellationToken = default);
        Task ConfirmSignUp
            (string email,
            string clientId,
            string code,
            CancellationToken cancellationToken = default);
        Task ForgotPasswordAsync
            (string email,
            CancellationToken cancellationToken = default);
        Task ConfirmForgotPassword
            (string email,
            string code,
            CancellationToken cancellationToken = default);
    }
}
