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
                    int userid = await (from u in _context.Users
                                        where u.Email == loginUser.Email && u.Password == loginUser.Password
                                        select u.Id).FirstAsync();
                    int roleid = await (from u in _context.Users
                                        where u.Id == userid
                                        select u.RoleId).FirstAsync();
                    tokenResult.expirationTime = DateTime.Now.AddMinutes(30);
                    tokenResult.roleId = roleid;
                    tokenResult.token = CustomTokenJWT(userid, roleid, loginUser.Email, tokenResult.expirationTime);
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

        // POST: api/Login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<Token>> PostUser(EventPlannerModels.User user)
        {
            Token tokenResult = new Token();
            try
            {
                if (_context.Users == null)
                {
                    return Problem("Entity set 'MoviesContext.Categories'  is null.");
                }
                Models.User context_user = new Models.User()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    RoleId = 3
                };
                _context.Users.Add(context_user);
                await _context.SaveChangesAsync();
                int userid = await (from u in _context.Users
                                    where u.Email == user.Email && u.Password == user.Password
                                    select u.Id).FirstAsync();
                int roleid = await (from u in _context.Users
                                    where u.Id == userid
                                    select u.RoleId).FirstAsync();
                tokenResult.expirationTime = DateTime.Now.AddMinutes(30);
                tokenResult.token = CustomTokenJWT(userid, roleid, user.Email, tokenResult.expirationTime);
                tokenResult.generalResult = new GeneralResult()
                {
                    Result = true
                };
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
        private string CustomTokenJWT(int userid, int roleid, string Email, DateTime token_expiration)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["JWT:SecretKey"])
                );

            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );

            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Sid, userid.ToString()),
                new Claim(ClaimTypes.Role, roleid.ToString())
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
