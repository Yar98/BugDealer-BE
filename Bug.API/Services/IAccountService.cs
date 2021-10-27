using Bug.API.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface IAccountService
    {
        Task<string> GenerateTokenAccountGoogle(AccountGoogleDto acc);
    }
}
