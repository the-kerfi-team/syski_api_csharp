using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Syski.API.Models;
using Syski.API.Services;
using Syski.Data;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Syski.API.Controllers
{
    public class UserAuthenticationController : ControllerBase
    {

        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly UserTokenManager tokenManager;

        public UserAuthenticationController(IConfiguration configuration, UserManager<ApplicationUser> userManager, UserTokenManager tokenManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.tokenManager = tokenManager;
        }

        [HttpPost("/auth/user/login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDTO userLoginDTO)
        {
            IActionResult result = null;
            if (!ModelState.IsValid)
            {
                result = BadRequest(ModelState);
            }
            else
            {
                ApplicationUser user = userManager.Users.SingleOrDefault(r => r.Email == userLoginDTO.Email);
                if (user != null)
                {
                    if (await userManager.CheckPasswordAsync(user, userLoginDTO.Password))
                    {
                        string refreshToken = null;
                        AuthenticationToken token = tokenManager.CreateToken(user, ref refreshToken);
                        result = Ok(new UserTokenDTO()
                        {
                            Id = token.User.Id,
                            Email = token.User.Email,
                            AccessToken = GenerateJwtToken(token),
                            RefreshToken = refreshToken,
                            Expiry = token.Expires
                        });
                    }
                    else
                    {
                        result = BadRequest();
                    }
                }
                else
                {
                    result = BadRequest();
                }
            }
            return result;
        }

        [HttpPost("/auth/user/register")]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterDTO registerDTO)
        {
            IActionResult result = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                ApplicationUser newUser = new ApplicationUser
                {
                    UserName = registerDTO.Email,
                    Email = registerDTO.Email
                };
                if (((IdentityResult) await userManager.CreateAsync(newUser, registerDTO.Password)).Succeeded)
                {
                    var user = userManager.Users.SingleOrDefault(r => r.Email == registerDTO.Email);
                    string refreshToken = null;
                    AuthenticationToken token = tokenManager.CreateToken(user, ref refreshToken);
                    result = Ok(new UserTokenDTO()
                    {
                        Id = token.User.Id,
                        Email = token.User.Email,
                        AccessToken = GenerateJwtToken(token),
                        RefreshToken = refreshToken,
                        Expiry = token.Expires
                    });
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            return result;
        }

        [HttpPost("/auth/user/token/refresh")]
        [Authorize("refreshtoken")]
        public IActionResult UserRefreshToken([FromBody] UserRefreshTokenDTO refreshTokenDTO)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (tokenManager.CheckValidRefreshToken(refreshTokenDTO.RefreshToken, claimsIdentity.FindFirst("jti").Value))
            {
                var user = userManager.Users.SingleOrDefault(r => r.Email == claimsIdentity.FindFirst("email").Value);
                string refreshToken = null;
                AuthenticationToken token = tokenManager.CreateToken(user, ref refreshToken, claimsIdentity.FindFirst("jti").Value);
                return Ok(new UserTokenDTO()
                {
                    Id = token.User.Id,
                    Email = token.User.Email,
                    AccessToken = GenerateJwtToken(token),
                    RefreshToken = refreshToken,
                    Expiry = token.Expires
                });
            }
            return BadRequest();
        }

        private string GenerateJwtToken(AuthenticationToken Token)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Token.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, Token.User.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
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
