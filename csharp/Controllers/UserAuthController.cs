using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using csharp.Data;
using csharp.Models;
using csharp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace csharp.Controllers
{
    [ApiController]
    public class UserAuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserTokenManager _tokenManager;

        public UserAuthController(UserManager<ApplicationUser> userManager, UserTokenManager tokenManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("/auth/user/login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userManager.Users.SingleOrDefault(r => r.Email == loginDTO.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                string refreshToken = null;
                Token token = _tokenManager.CreateToken(user, ref refreshToken);
                return Ok(new UserTokenDTO()
                {
                    Email = token.User.Email,
                    Token = GenerateJwtToken(token),
                    RefreshToken = refreshToken
                });
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("/auth/user/register")]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email
            };
            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (result.Succeeded)
            {
                var user = _userManager.Users.SingleOrDefault(r => r.Email == registerDTO.Email);
                string refreshToken = null;
                Token token = _tokenManager.CreateToken(user, ref refreshToken);
                return Ok(new UserTokenDTO()
                {
                    Email = token.User.Email,
                    Token = GenerateJwtToken(token),
                    RefreshToken = refreshToken
                });
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("/auth/user/token/refresh")]
        [Authorize ("refreshtoken")]
        public IActionResult UserRefreshToken([FromBody] UserRefreshTokenDTO refreshTokenDTO)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var someClaim = claimsIdentity.FindFirst("jti").Value;
            var user = _userManager.Users.SingleOrDefault(r => r.Email == claimsIdentity.FindFirst("email").Value);
            // Test if the refresh token is valid
            string refreshToken = null;
            Token token = _tokenManager.CreateToken(user, ref refreshToken);
            return Ok(new UserTokenDTO()
            {
                Email = token.User.Email,
                Token = GenerateJwtToken(token),
                RefreshToken = refreshToken
            });
        }

        private string GenerateJwtToken(Token Token)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Token.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, Token.User.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                //expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"])),
                expires: Token.Expires,
                notBefore: Token.NotBefore,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}