using System;

namespace Syski.Data
{
    public class AuthenticationToken
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

        public Guid? PreviousTokenId { get; set; }

    }
}
