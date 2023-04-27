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
    public class ItemCategoriesController : ControllerBase
    {
        private readonly EventPlanningContext _context;

        public ItemCategoriesController()
        {
            _context = new EventPlanningContext();
        }

        // GET: api/ItemCategories
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.ItemCategory>>> GetItemCategories()
        {
          if (_context.ItemCategories == null)
          {
              return NotFound();
          }
          var ItemCategories = await (from ic in _context.ItemCategories
                             select new EventPlannerModels.ItemCategory
                             {
                                 Id = ic.Id,
                                 Category = ic.Category,
                             }).ToListAsync();
            return ItemCategories;
        }

        // GET: api/ItemCategories/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventPlannerModels.ItemCategory>> GetItemCategory(int id)
        {
            if (_context.ItemCategories == null)
            {
                return NotFound();
            }
            var ItemCategory = await (from ic in _context.ItemCategories
                              where ic.Id == id
                               select new EventPlannerModels.ItemCategory
                               {
                                   Id = ic.Id,
                                   Category = ic.Category,
                               }).FirstAsync();

            if (ItemCategory == null)
            {
                return NotFound();
            }

            return ItemCategory;
        }

        // PUT: api/ItemCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ("1"))]
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResult>> PutItemCategory(EventPlannerModels.ItemCategory itemCategory)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                Models.ItemCategory context_ItemCategory = new Models.ItemCategory()
                {
                    Id = itemCategory.Id,
                    Category = itemCategory.Category,
                };
                _context.Entry(context_ItemCategory).State = EntityState.Modified;
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

        // POST: api/ItemCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ("1"))]
        [HttpPost]
        public async Task<ActionResult<GeneralResult>> PostItemCategory(EventPlannerModels.ItemCategory ItemCategory)
        {
            GeneralResult generalResult =
                      new GeneralResult() { Result = false };
            try
            {
                if (_context.Users == null)
                {
                    return Problem("Entity set 'MoviesContext.Categories'  is null.");
                }
                Models.ItemCategory context_ItemCategory = new Models.ItemCategory()
                {
                    Id = ItemCategory.Id,
                    Category = ItemCategory.Category,
                };
                _context.ItemCategories.Add(context_ItemCategory);
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

        // DELETE: api/ItemCategories/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ("1"))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResult>> DeleteItemCategory(int id)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                if (_context.ItemCategories == null)
                {
                    return NotFound();
                }
                var ItemCategory = await _context.ItemCategories.FindAsync(id);
                if (ItemCategory == null)
                {
                    return NotFound();
                }

                _context.ItemCategories.Remove(ItemCategory);
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
