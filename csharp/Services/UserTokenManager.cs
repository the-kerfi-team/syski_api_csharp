using csharp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace csharp.Services
{
    public class UserTokenManager
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserTokenManager(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public Token CreateToken(ApplicationUser user, ref string refreshToken)
        {
            Token token = GenerateToken(user, ref refreshToken);
            _context.Add(token);
            _context.SaveChanges();
            return token;
        }

        public Token CreateToken(ApplicationUser user, ref string refreshToken, ref Token previousToken)
        {
            Token token = GenerateToken(user, ref refreshToken);
            if (previousToken != null)
            {
                SetPreviousToken(ref token, ref previousToken);
            }
            _context.Add(token);
            _context.SaveChanges();
            return token;
        }

        public Token CreateToken(ApplicationUser user, ref string refreshToken, string previousTokenId)
        {
            Token token = GenerateToken(user, ref refreshToken);
            var previousToken = _context.Tokens.FirstOrDefault(t => t.Id == new Guid(previousTokenId));
            _context.Add(token);
            _context.SaveChanges();
            if (previousToken != null)
            {
                previousToken.Active = false;
                SetPreviousToken(ref token, ref previousToken);
                _context.SaveChanges();
            }
            return token;
        }

        public bool CheckValidRefreshToken(string refreshToken, string tokenId)
        {
            return _context.Tokens.Where(t => t.Id == new Guid(tokenId) && t.Active && t.RefreshToken == refreshToken).Any();
        }

        private void SetPreviousToken(ref Token nextToken, ref Token previousToken)
        {
            previousToken.NextTokenId = nextToken.Id;
            previousToken.NextToken = nextToken;
            nextToken.PreviousTokenId = previousToken.Id;
            nextToken.PreviousToken = previousToken;
        }

        private Token GenerateToken(ApplicationUser user, ref string refreshToken)
        {
            Token result = null;
            if (refreshToken == null)
            {
                refreshToken = GenerateRefreshToken();
            }
            result = new Token()
            {
                User = user,
                UserId = user.Id,
                TokenType = "Bearer",
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Subject = user.Email,
                Expires = DateTime.Now.AddMinutes(30),
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
