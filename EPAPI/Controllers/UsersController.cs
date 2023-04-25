using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventPlannerModels;
using EPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace EPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EventPlanningContext _context;

        public UsersController()
        {
            _context = new EventPlanningContext();
        }
        //// POST: api/Users/Login
        //[HttpPost]
        //[Route("Login")]
        //public async Task<ActionResult<GeneralResult>> Login(LoginUser loginUser)
        //{
        //    GeneralResult generalResult =
        //        new GeneralResult() { Result = false };
        //    try
        //    {
        //        if (await (from u in _context.Users
        //             where u.Email == loginUser.Email && u.Password == loginUser.Password
        //                   select 1).AnyAsync())
        //        {
        //            generalResult.Result = true;
        //        }
        //        else
        //        {
        //            generalResult.ErrorMessage = "Email o contraseña incorrectos";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        generalResult.Result = false;
        //        generalResult.ErrorMessage = ex.Message;
        //        throw;
        //    }
        //    return generalResult;
        //}
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
          var users = await (from u in _context.Users
                             select new EventPlannerModels.User
                             {
                                 Id = u.Id,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                             }).ToListAsync();
            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventPlannerModels.User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await (from u in _context.Users
                              where u.Id == id
                               select new EventPlannerModels.User
                               {
                                   Id = u.Id,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   Email = u.Email,
                                   Password = ""
                               }).FirstAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResult>> PutUser(int id, EventPlannerModels.User user)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                string password = (from u in _context.Users where u.Id == id select u.Password).First();
                Models.User context_user = new Models.User()
                {
                    Id = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MailVisible = user.MailVisible,
                    Email = user.Email,
                    PhoneVisible = user.PhoneVisible,
                    ContactPhone = user.ContactPhone,
                    Password = password,
                };
                _context.Entry(context_user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
                throw;
            }


            return generalResult;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GeneralResult>> PostUser(EventPlannerModels.User user)
        {
            GeneralResult generalResult =
                    new GeneralResult() { Result = false };
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
                };
                _context.Users.Add(context_user);
                await _context.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
                throw;
            }

            return generalResult;
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResult>> DeleteUser(int id)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                if (_context.Users == null)
                {
                    return NotFound();
                }
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
                throw;
            }
            return generalResult;
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
