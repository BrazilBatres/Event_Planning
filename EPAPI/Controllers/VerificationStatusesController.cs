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
    public class VerificationStatusesController : ControllerBase
    {
        private readonly EventPlanningContext _context;

        public VerificationStatusesController()
        {
            _context = new EventPlanningContext();
        }

        // GET: api/VerificationStatuses
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles =("1"))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.VerificationStatus>>> GetVerificationStatuses()
        {
          if (_context.VerificationStatuses == null)
          {
              return NotFound();
          }
          var VerificationStatuses = await (from r in _context.VerificationStatuses
                             select new EventPlannerModels.VerificationStatus
                             {
                                 Id = r.Id,
                                 Name = r.Name,
                             }).ToListAsync();
            return VerificationStatuses;
        }

        // GET: api/VerificationStatuses/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventPlannerModels.VerificationStatus>> GetVerificationStatus(int id)
        {
            if (_context.VerificationStatuses == null)
            {
                return NotFound();
            }
            var VerificationStatus = await (from vs in _context.VerificationStatuses
                              where vs.Id == id
                               select new EventPlannerModels.VerificationStatus
                               {
                                   Id = vs.Id,
                                   Name = vs.Name,
                               }).FirstAsync();

            if (VerificationStatus == null)
            {
                return NotFound();
            }

            return VerificationStatus;
        }
    }
}
