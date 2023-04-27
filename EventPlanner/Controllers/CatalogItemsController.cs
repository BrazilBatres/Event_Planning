using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventPlannerModels;
using Microsoft.AspNetCore.Authorization;

namespace EventPlanner.Controllers
{
    public class CatalogItemsController : Controller
    {

        public CatalogItemsController()
        {
        }

        // GET: CatalogItems
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            IEnumerable<CatalogItem> products = await Functions.APIServices.CatalogItemsGetList(token);
            return View(products);
        }

        // GET: CatalogItems/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            CatalogItem product = await Functions.APIServices.CatalogItemsDetails(id, token);
            return View(product);
        }

        // GET: CatalogItems/Create
        [Authorize(Roles ="2")]
        public async Task<IActionResult> Create()
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            IEnumerable<ItemCategory> productCategories = await Functions.APIServices.ItemCategoriesGetList(token);
            ViewData["ItemCategoryId"] = new SelectList(productCategories, "Id", "Category");
            
            return View();
        }

        // POST: CatalogItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Create([Bind("SellerId,CatalogItemName,CatalogItemDescription,CatalogItemPrice,CatalogItemCategoryId")] CatalogItem product)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.CatalogItemsCreate(product, token);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: CatalogItems/Edit/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Edit(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            CatalogItem product = await Functions.APIServices.CatalogItemsDetails(id, token);
            IEnumerable<ItemCategory> productCategories = await Functions.APIServices.ItemCategoriesGetList(token);
            ViewData["ItemCategoryId"] = productCategories.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Category,
                Selected = (s.Id == product.ItemCategoryId)
            }).ToList();
            return View(product);
        }

        // POST: CatalogItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, SellerId,CatalogItemName,CatalogItemDescription,CatalogItemPrice,CatalogItemCategoryId")] CatalogItem product)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.CatalogItemsEdit(id, product, token);
            if (generalResult.Result)
            {

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: CatalogItems/Delete/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Delete(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            CatalogItem product = await Functions.APIServices.CatalogItemsDetails(id, token);

            return View(product);
        }

        // POST: CatalogItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.CatalogItemsDelete(id, token);
            return RedirectToAction(nameof(Index));
        }

    }
}
