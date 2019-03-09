using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class ApplicationUser : IdentityUser
    {

        public virtual ICollection<Token> Tokens { get; set; }

        public virtual ICollection<ApplicationUserSystems> Systems { get; set; }

    }
}
