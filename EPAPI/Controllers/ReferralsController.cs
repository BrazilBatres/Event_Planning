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
using System.Security.Claims;
using System.Security.Principal;

namespace EPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferralsController : ControllerBase
    {
        private readonly EventPlanningContext _context;

        public ReferralsController()
        {
            _context = new EventPlanningContext();
        }

        // GET: api/Referrals
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.Referral>>> GetReferrals()
        {
            if (_context.Referrals == null)
            {
                return NotFound();
            }
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userid = identity.Claims.First(u => u.Type == ClaimTypes.Sid).Value;
            int sellerid = await (from s in _context.Sellers
                                  where s.UserId == int.Parse(userid)
                                  select s.Id).FirstOrDefaultAsync();
            var Referrals = await (from r in _context.Referrals
                                   where r.SellerId == sellerid
                                             select new EventPlannerModels.Referral
                                             {
                                                 Id = r.Id,
                                                 SellerId = r.SellerId,
                                                 Name = r.Name,
                                                 Phone = r.Phone,
                                                 Email = r.Email,
                                             }).ToListAsync();
            return Referrals;
        }
        // GET: api/Referrals
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles =("1"))]
        [HttpGet]
        [Route("RefByUser/{sellerid}")]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.Referral>>> GetReferrals(int sellerid)
        {
            if (_context.Referrals == null)
            {
                return NotFound();
            }
            var Referrals = await (from r in _context.Referrals
                                   where r.SellerId == sellerid
                                   select new EventPlannerModels.Referral
                                   {
                                       Id = r.Id,
                                       SellerId = r.SellerId,
                                       Name = r.Name,
                                       Phone = r.Phone,
                                       Email = r.Email,
                                   }).ToListAsync();
            return Referrals;
        }

        // GET: api/Referrals/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventPlannerModels.Referral>> GetReferral(int id)
        {
            if (_context.Referrals == null)
            {
                return NotFound();
            }
            var Referral = await (from r in _context.Referrals
                                            where r.Id == id
                                            select new EventPlannerModels.Referral
                                            {
                                                Id = r.Id,
                                                SellerId = r.SellerId,
                                                Name = r.Name,
                                                Phone = r.Phone,
                                                Email = r.Email,
                                            }).FirstAsync();

            if (Referral == null)
            {
                return NotFound();
            }

            return Referral;
        }

        // PUT: api/Referrals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResult>> PutReferral(EventPlannerModels.Referral referral)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                string userid = identity.Claims.First(u => u.Type == ClaimTypes.Sid).Value;
                int sellerid = await (from s in _context.Sellers
                                      where s.UserId == int.Parse(userid)
                                      select s.Id).FirstOrDefaultAsync();
                Models.Referral context_Referral = new Models.Referral()
                {
                    Id = referral.Id,
                    SellerId = sellerid,
                    Name = referral.Name,
                    Phone = referral.Phone,
                    Email = referral.Email,
                };
                _context.Entry(context_Referral).State = EntityState.Modified;
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

        // POST: api/Referrals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<GeneralResult>> PostReferral(EventPlannerModels.Referral Referral)
        {
            GeneralResult generalResult =
                      new GeneralResult() { Result = false };
            try
            {
                if (_context.Users == null)
                {
                    return Problem("Entity set 'EventPlanningContext.Referral'  is null.");
                }
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                string userid = identity.Claims.First(u => u.Type == ClaimTypes.Sid).Value;
                int sellerid = await (from s in _context.Sellers
                                      where s.UserId == int.Parse(userid)
                                      select s.Id).FirstOrDefaultAsync();
                Models.Referral context_Referral = new Models.Referral()
                {
                    Id = Referral.Id,
                    SellerId = sellerid,
                    Name = Referral.Name,
                    Phone = Referral.Phone,
                    Email = Referral.Email
                };
                _context.Referrals.Add(context_Referral);
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

        // DELETE: api/Referrals/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResult>> DeleteReferral(int id)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                if (_context.Referrals == null)
                {
                    return NotFound();
                }
                var Referral = await _context.Referrals.FindAsync(id);
                if (Referral == null)
                {
                    return NotFound();
                }

                _context.Referrals.Remove(Referral);
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
