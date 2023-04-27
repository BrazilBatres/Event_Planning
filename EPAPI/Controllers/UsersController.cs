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
using System.Threading.Tasks.Dataflow;

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

        // GET: api/Users
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ("1"))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
          var users = await (from u in _context.Users
                             join r in _context.Roles on u.RoleId equals r.Id
                             select new EventPlannerModels.User
                             {
                                 Id = u.Id,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 RoleId = u.RoleId,
                                 Role = new EventPlannerModels.Role()
                                 {
                                     RoleName = r.RoleName
                                 }
                             }).ToListAsync();
            return users;
        }

        // GET: api/Users/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventPlannerModels.User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await (from u in _context.Users
                              join r in _context.Roles on u.RoleId equals r.Id
                              where u.Id == id
                               select new EventPlannerModels.User
                               {
                                   Id = u.Id,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   Email = u.Email,
                                   RoleId = u.RoleId,
                                   Password = "",
                                   Role = new EventPlannerModels.Role()
                                   {
                                       RoleName = r.RoleName
                                   },
                               }).FirstAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                    RoleId = user.RoleId,
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

        

        // DELETE: api/Users/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    }
}
