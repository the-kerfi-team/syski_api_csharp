using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class Token
    {

        public Guid Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string TokenType { get; set; }

        public string Issuer { get; set; }
    
        public string Audience { get; set; }

        public string Subject { get; set; }

        public DateTime Expires { get; set; }

        public DateTime NotBefore { get; set; }

        public string RefreshToken { get; set; }

        public bool Active { get; set; }

        public Guid? NextTokenId { get; set; }

        public Token NextToken { get; set; }

        public Guid? PreviousTokenId { get; set; }

        public Token PreviousToken { get; set; }

    }
}
