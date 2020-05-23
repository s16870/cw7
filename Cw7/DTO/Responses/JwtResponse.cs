using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.DTO.Responses
{
    public class JwtResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
