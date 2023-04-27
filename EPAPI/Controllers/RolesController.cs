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
    public class RolesController : ControllerBase
    {
        private readonly EventPlanningContext _context;

        public RolesController()
        {
            _context = new EventPlanningContext();
        }

        // GET: api/Roles
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles =("1"))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.Role>>> GetRoles()
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
          var Roles = await (from r in _context.Roles
                             select new EventPlannerModels.Role
                             {
                                 Id = r.Id,
                                 RoleName = r.RoleName,
                             }).ToListAsync();
            return Roles;
        }

        // GET: api/Roles/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventPlannerModels.Role>> GetRole(int id)
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }
            var Role = await (from r in _context.Roles
                              where r.Id == id
                               select new EventPlannerModels.Role
                               {
                                   Id = r.Id,
                                   RoleName = r.RoleName,
                               }).FirstAsync();

            if (Role == null)
            {
                return NotFound();
            }

            return Role;
        }
    }
}
