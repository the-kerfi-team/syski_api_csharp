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
            // Get previous token from database
            //if (previousToken != null)
            //{
            //    SetPreviousToken(ref token, ref previousToken);
            //}
            token.PreviousTokenId = new Guid(previousTokenId);
            _context.Add(token);
            _context.SaveChanges();
            return token;
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
                RefreshToken = GenerateHash(refreshToken),
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

        public static string GenerateHash(string hash)
        {
            byte[] salt;
            byte[] bytes;
            if (hash == null)
            {
                throw new ArgumentNullException("Hash");
            }
            using (Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(hash, 16, 1000))
            {
                salt = rfc2898DeriveByte.Salt;
                bytes = rfc2898DeriveByte.GetBytes(32);
            }
            byte[] numArray = new byte[49];
            Buffer.BlockCopy(salt, 0, numArray, 1, 16);
            Buffer.BlockCopy(bytes, 0, numArray, 17, 32);
            return Convert.ToBase64String(numArray);
        }

    }
}
