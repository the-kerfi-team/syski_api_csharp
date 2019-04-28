using Microsoft.Extensions.Configuration;
using Syski.Data;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Syski.API.Services
{
    public class UserTokenManager
    {

        private readonly SyskiDBContext context;
        private readonly IConfiguration configuration;

        public UserTokenManager(SyskiDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }


        public AuthenticationToken CreateToken(ApplicationUser user, ref string refreshToken)
        {
            AuthenticationToken token = GenerateToken(user, ref refreshToken);
            context.Add(token);
            context.SaveChanges();
            return token;
        }

        public AuthenticationToken CreateToken(ApplicationUser user, ref string refreshToken, string previousTokenId)
        {
            AuthenticationToken token = GenerateToken(user, ref refreshToken);
            var previousToken = context.AuthenticationTokens.FirstOrDefault(t => t.Id == new Guid(previousTokenId));
            context.Add(token);
            context.SaveChanges();
            if (previousToken != null)
            {
                previousToken.Active = false;
                SetPreviousToken(ref token, ref previousToken);
                context.SaveChanges();
            }
            return token;
        }

        public bool CheckValidRefreshToken(string refreshToken, string tokenId)
        {
            return context.AuthenticationTokens.Any(t => t.Id == new Guid(tokenId) && t.Active && t.RefreshToken == refreshToken);
        }

        private void SetPreviousToken(ref AuthenticationToken nextToken, ref AuthenticationToken previousToken)
        {
            previousToken.NextTokenId = nextToken.Id;
            nextToken.PreviousTokenId = previousToken.Id;
        }

        private AuthenticationToken GenerateToken(ApplicationUser user, ref string refreshToken)
        {
            AuthenticationToken result = null;
            if (refreshToken == null)
            {
                refreshToken = GenerateRefreshToken();
            }
            result = new AuthenticationToken()
            {
                Id = new Guid(),
                User = user,
                UserId = user.Id,
                TokenType = "Bearer",
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                Subject = user.Email,
                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:ExpireInMinutes"])),
                NotBefore = DateTime.Now,
                RefreshToken = refreshToken,
                Active = true
            };
            return (result);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[128];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

    }
}
