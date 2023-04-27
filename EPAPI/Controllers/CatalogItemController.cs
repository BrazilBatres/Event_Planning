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

namespace EPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogItemsController : ControllerBase
    {
        private readonly EventPlanningContext _context;

        public CatalogItemsController()
        {
            _context = new EventPlanningContext();
        }

        
        // GET: api/CatalogItems
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.CatalogItem>>> GetCatalogItems()
        {
            if (_context.CatalogItems == null)
            {
                return NotFound();
            }
            var CatalogItems = await (from p in _context.CatalogItems
                               join pc in _context.ItemCategories on p.ItemCategoryId equals pc.Id
                               join u in _context.Users on p.SellerId equals u.Id
                               select new EventPlannerModels.CatalogItem
                               {
                                   Id = p.Id,
                                   SellerId = p.SellerId,
                                   ItemName = p.ItemName,
                                   ItemDescription = p.ItemDescription,
                                   ItemPrice = p.ItemPrice,
                                   ItemCategoryId = p.ItemCategoryId,
                                   ItemCategory = new EventPlannerModels.ItemCategory
                                   {
                                       Id = pc.Id,
                                       Category = pc.Category
                                   },
                                   Seller = new EventPlannerModels.User
                                   {
                                       Id = u.Id,
                                       FirstName = u.FirstName,
                                       LastName = u.LastName,
                                   }

                               }).ToListAsync();
            return CatalogItems;
        }

        // GET: api/CatalogItems/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles =("2"))]
        [HttpGet]
        [Route("{userid}")]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.CatalogItem>>> MyCatalogItems(int userid)
        {
            if (_context.CatalogItems == null)
            {
                return NotFound();
            }
            var CatalogItems = await (from p in _context.CatalogItems
                                      join pc in _context.ItemCategories on p.ItemCategoryId equals pc.Id
                                      join u in _context.Users on p.SellerId equals u.Id
                                      where u.Id == userid
                                      select new EventPlannerModels.CatalogItem
                                      {
                                          Id = p.Id,
                                          SellerId = p.SellerId,
                                          ItemName = p.ItemName,
                                          ItemDescription = p.ItemDescription,
                                          ItemPrice = p.ItemPrice,
                                          ItemCategoryId = p.ItemCategoryId,
                                          ItemCategory = new EventPlannerModels.ItemCategory
                                          {
                                              Id = pc.Id,
                                              Category = pc.Category
                                          },
                                          Seller = new EventPlannerModels.User
                                          {
                                              Id = u.Id,
                                              FirstName = u.FirstName,
                                              LastName = u.LastName,
                                          }

                                      }).ToListAsync();
            return CatalogItems;
        }

        // GET: api/CatalogItems/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventPlannerModels.CatalogItem>> GetCatalogItem(int id)
        {
            if (_context.CatalogItems == null)
            {
                return NotFound();
            }
            var CatalogItem = await (from p in _context.CatalogItems
                                     join pc in _context.ItemCategories on p.ItemCategoryId equals pc.Id
                                     join u in _context.Users on p.SellerId equals u.Id
                                     where p.Id == id
                                     select new EventPlannerModels.CatalogItem
                                     {
                                         Id = p.Id,
                                         SellerId = p.SellerId,
                                         ItemName = p.ItemName,
                                         ItemDescription = p.ItemDescription,
                                         ItemPrice = p.ItemPrice,
                                         ItemCategoryId = p.ItemCategoryId,
                                         ItemCategory = new EventPlannerModels.ItemCategory
                                         {
                                             Id = pc.Id,
                                             Category = pc.Category
                                         },
                                         Seller = new EventPlannerModels.User
                                         {
                                             Id = u.Id,
                                             FirstName = u.FirstName,
                                             LastName = u.LastName,
                                         }

                                     }).FirstOrDefaultAsync();

            if (CatalogItem == null)
            {
                return NotFound();
            }

            return CatalogItem;
        }

        // PUT: api/CatalogItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles =("2"))]
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResult>> PutCatalogItem(int id, EventPlannerModels.CatalogItem CatalogItem)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                Models.CatalogItem context_CatalogItem = new Models.CatalogItem()
                {
                    Id = id,
                    SellerId= CatalogItem.SellerId,
                    ItemName = CatalogItem.ItemName,
                    ItemDescription = CatalogItem.ItemDescription,
                    ItemPrice = CatalogItem.ItemPrice,
                    ItemCategoryId = CatalogItem.ItemCategoryId,
                };
                _context.Entry(context_CatalogItem).State = EntityState.Modified;
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

        // POST: api/CatalogItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles =("2"))]
        [HttpPost]
        public async Task<ActionResult<GeneralResult>> PostCatalogItem(EventPlannerModels.CatalogItem CatalogItem)
        {
            GeneralResult generalResult =
                      new GeneralResult() { Result = false };
            try
            {
                if (_context.Users == null)
                {
                    return Problem("Entity set 'MoviesContext.Categories'  is null.");
                }
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                string userid = identity.Claims.First(u => u.Type == ClaimTypes.Sid).Value;
                Models.CatalogItem context_CatalogItem = new Models.CatalogItem()
                {
                    Id = CatalogItem.Id,
                    SellerId = int.Parse(userid),
                    ItemName = CatalogItem.ItemName,
                    ItemDescription = CatalogItem.ItemDescription,
                    ItemPrice = CatalogItem.ItemPrice,
                    ItemCategoryId = CatalogItem.ItemCategoryId,
                };
                _context.CatalogItems.Add(context_CatalogItem);
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

        // DELETE: api/CatalogItems/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ("2"))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResult>> DeleteCatalogItem(int id)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                if (_context.Users == null)
                {
                    return NotFound();
                }
                var CatalogItem = await _context.CatalogItems.FindAsync(id);
                if (CatalogItem == null)
                {
                    return NotFound();
                }

                _context.CatalogItems.Remove(CatalogItem);
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
