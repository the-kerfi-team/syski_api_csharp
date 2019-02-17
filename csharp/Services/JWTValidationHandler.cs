using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Services
{
    public class JWTValidationHandler : AuthorizationHandler<JWTValidation>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JWTValidation requirement)
        {
            throw new NotImplementedException();
        }

    }
}
