using Bug.API.Dto;
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
        string GenerateToken(string id, string name, string email, bool isRemember);
        public AccountBtsJwtDto ValidateToken(string token);
    }
}
