using Cw7.DTO.Requests;
using Cw7.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.Services
{
    public interface IJwtAuthorizationService
    {
        public JwtResponse LogIn(LoginRequest request);
        public JwtResponse RefreshToken(String refreshToken);
    }
}
