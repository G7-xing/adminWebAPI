using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service.Jwt
{
    public interface IJwtService
    {
        public string GenerateToken(User loginReq);
        public LoginRes verityToken(string token);
    }
}
