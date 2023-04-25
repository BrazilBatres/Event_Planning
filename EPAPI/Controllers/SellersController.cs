using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventPlannerModels;
using EPAPI.Models;

namespace EPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly EventPlanningContext _context;

        public SellersController()
        {
            _context = new EventPlanningContext();
        }

        // GET: api/Sellers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.Seller>>> GetSellers()
        {
            if (_context.Sellers == null)
            {
                return NotFound();
            }
            var sellers = await (from s in _context.Sellers
                                 join u in _context.Users on s.UserId equals u.Id
                               select new EventPlannerModels.Seller
                               {
                                   Id = s.Id,
                                   UserId = s.UserId,
                                   CompanyName = s.CompanyName,
                                   IdentificationTypeId = s.IdentificationTypeId,
                                   IdentificationNumber = s.IdentificationNumber,
                                   ExperienceYears = s.ExperienceYears,
                                   Freelance = s.Freelance,
                                   User = new EventPlannerModels.User() {
                                       FirstName = u.FirstName,
                                       LastName = u.LastName,
                                   }
                               }).ToListAsync();
            return sellers;
        }

        // GET: api/Sellers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventPlannerModels.Seller>> GetSeller(int id)
        {
            if (_context.Sellers == null)
            {
                return NotFound();
            }
            var seller = await (from s in _context.Sellers
                                join u in _context.Users on s.UserId equals u.Id
                                select new EventPlannerModels.Seller
                                {
                                    Id = s.Id,
                                    UserId = s.UserId,
                                    CompanyName = s.CompanyName,
                                    IdentificationTypeId = s.IdentificationTypeId,
                                    IdentificationNumber = s.IdentificationNumber,
                                    ExperienceYears = s.ExperienceYears,
                                    Freelance = s.Freelance,
                                    User = new EventPlannerModels.User()
                                    {
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                    }
                                }).FirstAsync();

            if (seller == null)
            {
                return NotFound();
            }

            return seller;
        }

        // PUT: api/Sellers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResult>> PutSeller(int id, EventPlannerModels.Seller seller)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                
                Models.Seller context_seller = new Models.Seller()
                {
                    Id = id,
                    UserId = seller.UserId,
                    CompanyName = seller.CompanyName,
                    IdentificationTypeId = seller.IdentificationTypeId,
                    IdentificationNumber = seller.IdentificationNumber,
                    ExperienceYears = seller.ExperienceYears,
                    Freelance = seller.Freelance,
                };
                _context.Entry(context_seller).State = EntityState.Modified;
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

        // POST: api/Sellers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GeneralResult>> PostSeller(EventPlannerModels.Seller seller)
        {
            GeneralResult generalResult =
                      new GeneralResult() { Result = false };
            try
            {
                if (_context.Sellers == null)
                {
                    return Problem("Entity set 'MoviesContext.Categories'  is null.");
                }
                Models.Seller context_seller = new Models.Seller()
                {
                    Id = seller.Id,
                    UserId = seller.UserId,
                    CompanyName = seller.CompanyName,
                    IdentificationTypeId = seller.IdentificationTypeId,
                    IdentificationNumber = seller.IdentificationNumber,
                    ExperienceYears = seller.ExperienceYears,
                    Freelance = seller.Freelance,
                };
                _context.Sellers.Add(context_seller);
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

        // DELETE: api/Sellers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResult>> DeleteSeller(int id)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                if (_context.Sellers == null)
                {
                    return NotFound();
                }
                var seller = await _context.Sellers.FindAsync(id);
                if (seller == null)
                {
                    return NotFound();
                }

                _context.Sellers.Remove(seller);
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
