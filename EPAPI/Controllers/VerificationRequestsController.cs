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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationRequestsController : ControllerBase
    {
        private readonly EventPlanningContext _context;

        public VerificationRequestsController()
        {
            _context = new EventPlanningContext();
        }

        // GET: api/VerificationRequests
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles =("1"))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.VerificationRequest>>> GetVerificationRequests()
        {
            if (_context.VerificationRequests == null)
            {
                return NotFound();
            }
            var VerificationRequests = await (from vr in _context.VerificationRequests
                                              join s in _context.Sellers on vr.SellerId equals s.Id
                                              join u1 in _context.Users on s.UserId equals u1.Id
                                              join vs in _context.VerificationStatuses on vr.StatusId equals vs.Id
                                   select new EventPlannerModels.VerificationRequest
                                   {
                                       Id = vr.Id,
                                       SellerId = vr.SellerId,
                                       AdminId = vr.AdminId,
                                       Description = vr.Description,
                                       AdminComments = vr.AdminComments,
                                       StatusId = vr.StatusId,
                                       TransacDate = vr.TransacDate,
                                       Seller = new EventPlannerModels.Seller()
                                       {
                                           User = new EventPlannerModels.User()
                                           {
                                               FirstName = u1.FirstName
                                               , LastName = u1.LastName,
                                           }
                                       },
                                       Status = new EventPlannerModels.VerificationStatus()
                                       {
                                           Name = vs.Name
                                       }
                                   }).ToListAsync();
            foreach (var verificationRequest in VerificationRequests)
            {
                if (verificationRequest.AdminId != null)
                {
                    verificationRequest.Admin = await (from u in _context.Users
                                                       where u.Id == verificationRequest.AdminId
                                                       select new EventPlannerModels.User()
                                                       {
                                                           FirstName = u.FirstName,
                                                           LastName = u.LastName,
                                                       }).FirstAsync();
                }
            }
            return VerificationRequests;
        }

        // GET: api/VerificationRequests/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventPlannerModels.VerificationRequest>> GetVerificationRequest(int id)
        {
            if (_context.VerificationRequests == null)
            {
                return NotFound();
            }
            var verificationRequest = await (from vr in _context.VerificationRequests
                                             join s in _context.Sellers on vr.SellerId equals s.Id
                                             join u1 in _context.Users on s.UserId equals u1.Id
                                             join vs in _context.VerificationStatuses on vr.StatusId equals vs.Id
                                             where vr.Id == id
                                             select new EventPlannerModels.VerificationRequest
                                             {
                                                 Id = vr.Id,
                                                 SellerId = vr.SellerId,
                                                 AdminId = vr.AdminId,
                                                 Description = vr.Description,
                                                 AdminComments = vr.AdminComments,
                                                 StatusId = vr.StatusId,
                                                 TransacDate = vr.TransacDate,
                                                 Seller = new EventPlannerModels.Seller()
                                                 {
                                                     User = new EventPlannerModels.User()
                                                     {
                                                         FirstName = u1.FirstName
                                                         ,
                                                         LastName = u1.LastName,
                                                     }
                                                 },
                                                 Status = new EventPlannerModels.VerificationStatus()
                                                 {
                                                     Name = vs.Name
                                                 }
                                             }).FirstAsync();
            if (verificationRequest.AdminId != null)
            {
                verificationRequest.Admin = await (from u in _context.Users
                                                   where u.Id == verificationRequest.AdminId
                                                   select new EventPlannerModels.User()
                                                   {
                                                       FirstName = u.FirstName,
                                                       LastName = u.LastName,
                                                   }).FirstAsync();
            }
            if (verificationRequest == null)
            {
                return NotFound();
            }

            return verificationRequest;
        }

        // PUT: api/VerificationRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ("1"))]
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResult>> PutVerificationRequest(EventPlannerModels.VerificationRequest verificationRequest)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                var actual_VR = await (from vr in _context.VerificationRequests
                                                                                where vr.Id == verificationRequest.Id
                                                                                select new EventPlannerModels.VerificationRequest()
                                                                                {
                                                                                    Description = vr.Description,
                                                                                    AdminComments = vr.AdminComments,
                                                                                    StatusId = vr.StatusId,
                                                                                }).FirstAsync();
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                string userid = identity.Claims.First(u => u.Type == ClaimTypes.Sid).Value;
                int sellerid = await (from s in _context.Sellers
                                      where s.UserId == int.Parse(userid)
                                      select s.Id).FirstOrDefaultAsync();
                Models.VerificationRequest context_VR;
                if (actual_VR.StatusId != verificationRequest.StatusId || actual_VR.AdminComments != verificationRequest.AdminComments)
                {
                    context_VR = new Models.VerificationRequest()
                    {
                        Id = verificationRequest.Id,
                        SellerId = verificationRequest.SellerId,
                        AdminId = int.Parse(userid),
                        Description = actual_VR.Description,
                        AdminComments = verificationRequest.AdminComments,
                        StatusId = verificationRequest.StatusId,
                        TransacDate = DateTime.Now,
                    };
                    if (verificationRequest.StatusId == 2)
                    {
                        Models.User user = await (from u in _context.Users
                                                  join s in _context.Sellers on u.Id equals s.UserId
                                                  where s.Id == verificationRequest.SellerId
                                                  select new Models.User()
                                                  {
                                                      Id = u.Id,
                                                      FirstName = u.FirstName,
                                                      LastName = u.LastName,
                                                      MailVisible = u.MailVisible,
                                                      Email = u.Email,
                                                      PhoneVisible = u.PhoneVisible,
                                                      ContactPhone = u.ContactPhone,
                                                      Password = u.Password,
                                                      RoleId = 2,
                                                  }).FirstAsync();
                        _context.Entry(user).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    context_VR = new Models.VerificationRequest()
                    {
                        Id = verificationRequest.Id,
                        SellerId = verificationRequest.SellerId,
                        AdminId = verificationRequest.AdminId,
                        Description = verificationRequest.Description,
                        AdminComments = actual_VR.AdminComments,
                        StatusId = actual_VR.StatusId,
                        TransacDate = DateTime.Now,
                    };
                }
                   
                _context.Entry(context_VR).State = EntityState.Modified;
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

        // POST: api/VerificationRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<GeneralResult>> PostVerificationRequest(EventPlannerModels.VerificationRequest verificationRequest)
        {
            GeneralResult generalResult =
                      new GeneralResult() { Result = false };
            try
            {
                if (_context.Users == null)
                {
                    return Problem("Entity set 'EventPlanningContext.VerificationRequest'  is null.");
                }
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                string userid = identity.Claims.First(u => u.Type == ClaimTypes.Sid).Value;
                int sellerid = await (from s in _context.Sellers
                                      where s.UserId == int.Parse(userid)
                                      select s.Id).FirstOrDefaultAsync();
                Models.VerificationRequest context_VerificationRequest = new Models.VerificationRequest()
                {
                    Id = verificationRequest.Id,
                    SellerId = sellerid,
                    AdminId = null,
                    Description = verificationRequest.Description,
                    AdminComments = "",
                    StatusId = 1,
                    TransacDate = DateTime.Now,
                };
                _context.VerificationRequests.Add(context_VerificationRequest);
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

        // DELETE: api/VerificationRequests/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ("1"))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResult>> DeleteVerificationRequest(int id)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                if (_context.VerificationRequests == null)
                {
                    return NotFound();
                }
                var VerificationRequest = await _context.VerificationRequests.FindAsync(id);
                if (VerificationRequest == null)
                {
                    return NotFound();
                }

                _context.VerificationRequests.Remove(VerificationRequest);
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
