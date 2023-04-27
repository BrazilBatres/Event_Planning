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
    public class IdentificationTypesController : ControllerBase
    {
        private readonly EventPlanningContext _context;

        public IdentificationTypesController()
        {
            _context = new EventPlanningContext();
        }

        // GET: api/IdentificationTypes
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.IdentificationType>>> GetIdentificationTypes()
        {
          if (_context.IdentificationTypes == null)
          {
              return NotFound();
          }
          var IdentificationTypes = await (from it in _context.IdentificationTypes
                             select new EventPlannerModels.IdentificationType
                             {
                                 Id = it.Id,
                                 Name = it.Name,
                             }).ToListAsync();
            return IdentificationTypes;
        }

        // GET: api/IdentificationTypes/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventPlannerModels.IdentificationType>> GetIdentificationType(int id)
        {
            if (_context.IdentificationTypes == null)
            {
                return NotFound();
            }
            var IdentificationType = await (from it in _context.IdentificationTypes
                              where it.Id == id
                               select new EventPlannerModels.IdentificationType
                               {
                                   Id = it.Id,
                                   Name = it.Name,
                               }).FirstAsync();

            if (IdentificationType == null)
            {
                return NotFound();
            }

            return IdentificationType;
        }

        // PUT: api/IdentificationTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResult>> PutIdentificationType(EventPlannerModels.IdentificationType identificationType)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                Models.IdentificationType context_IdentificationType = new Models.IdentificationType()
                {
                    Id = identificationType.Id,
                    Name = identificationType.Name,
                };
                _context.Entry(context_IdentificationType).State = EntityState.Modified;
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

        // POST: api/IdentificationTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<GeneralResult>> PostIdentificationType(EventPlannerModels.IdentificationType IdentificationType)
        {
            GeneralResult generalResult =
                      new GeneralResult() { Result = false };
            try
            {
                if (_context.Users == null)
                {
                    return Problem("Entity set 'MoviesContext.Categories'  is null.");
                }
                Models.IdentificationType context_IdentificationType = new Models.IdentificationType()
                {
                    Id = IdentificationType.Id,
                    Name = IdentificationType.Name,
                };
                _context.IdentificationTypes.Add(context_IdentificationType);
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

        // DELETE: api/IdentificationTypes/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResult>> DeleteIdentificationType(int id)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                if (_context.IdentificationTypes == null)
                {
                    return NotFound();
                }
                var IdentificationType = await _context.IdentificationTypes.FindAsync(id);
                if (IdentificationType == null)
                {
                    return NotFound();
                }

                _context.IdentificationTypes.Remove(IdentificationType);
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
