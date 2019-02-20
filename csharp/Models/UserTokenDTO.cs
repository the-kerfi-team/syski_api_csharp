using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Models
{
    public class UserTokenDTO
    {

        public string Id { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

    }
}
