using EventPlannerModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EPAPI.Models;

namespace EPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAPIController : Controller
    {
        private readonly IConfiguration _config;
        private readonly EventPlanningContext _context;

        public LoginAPIController(IConfiguration config)
        {
            _config = config;
            _context = new EventPlanningContext();
        }
        [Route("Login")]
        [HttpPost]
        public async Task<EventPlannerModels.Token> Login(LoginUser loginUser)
        {
            EventPlannerModels.Token tokenResult = new EventPlannerModels.Token();
            try
            {
                if (await (from u in _context.Users
                           where u.Email == loginUser.Email && u.Password == loginUser.Password
                           select 1).AnyAsync())//Aquí verificar que la contraseña y usuarios sean correctos
                {
                    string applicationName = "EPAPI";
                    tokenResult.expirationTime = DateTime.Now.AddMinutes(30);
                    tokenResult.token = CustomTokenJWT(applicationName, tokenResult.expirationTime);
                    tokenResult.generalResult = new GeneralResult()
                    {
                        Result = true
                    };
                }
                else
                {
                    tokenResult.generalResult = new GeneralResult()
                    {
                        Result = false,
                        ErrorMessage = "Email o contraseña incorrectos"
                    };
                }
            }
            catch (Exception ex)
            {
                tokenResult.generalResult = new GeneralResult()
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
                throw;
            }
            return tokenResult;
        }
        private string CustomTokenJWT(string ApplicationName, DateTime token_expiration)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["JWT:SecretKey"])
                );

            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );

            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, ApplicationName)
            };

            var _Payload = new JwtPayload(
                    issuer: _config["JWT:Issuer"],
                    audience: _config["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.Now,
                    expires: token_expiration
                );

            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);

        }
    }
}
