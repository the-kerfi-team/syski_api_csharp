using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Syski.Data
{
    public class ApplicationUser : IdentityUser
    {

        public IEnumerable<ApplicationUserSystems> Systems { get; set; }

        public IEnumerable<AuthenticationToken> AuthenticationTokens { get; set; }

    }
}
