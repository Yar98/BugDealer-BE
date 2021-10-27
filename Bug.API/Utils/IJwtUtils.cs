using Bug.API.Services.DTO;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bug.API.Utils
{
    public interface IJwtUtils
    {
        public string GenerateToken(string id, string name, string email);
        public AccountJwtDto ValidateToken(string token);
    }
}
