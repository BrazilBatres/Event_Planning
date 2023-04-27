using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventPlannerModels;
//Para autenticación de usuarios
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EventPlanner.Controllers
{
    
    public class ItemCategoriesController : Controller
    {


        public ItemCategoriesController() { }


        // GET: ItemCategories
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            IEnumerable<ItemCategory> ItemCategories = await Functions.APIServices.ItemCategoriesGetList(token);

            return View(ItemCategories);
        }

        // GET: ItemCategories/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            ItemCategory ItemCategory = await Functions.APIServices.ItemCategoriesDetails(id, token);
            return View(ItemCategory);
        }

        // GET: ItemCategories/Create
        [Authorize(Roles =("1"))]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ItemCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("1"))]
        public async Task<IActionResult> Create([Bind("Id,Category")] ItemCategory ItemCategory)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.ItemCategoriesCreate(ItemCategory, token);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(ItemCategory);
        }
        // GET: ItemCategories/Edit/5
        [Authorize(Roles = ("1"))]
        public async Task<IActionResult> Edit(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            ItemCategory ItemCategory = await Functions.APIServices.ItemCategoriesDetails(id, token);
            return View(ItemCategory);
        }

        // POST: ItemCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("1"))]
        public async Task<IActionResult> Edit([Bind("Id,Category")] ItemCategory ItemCategory)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.ItemCategoriesEdit(ItemCategory, token);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(ItemCategory);
        }

        // GET: ItemCategories/Delete/5
        [Authorize(Roles = ("1"))]
        public async Task<IActionResult> Delete(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            ItemCategory ItemCategory = await Functions.APIServices.ItemCategoriesDetails(id, token);

            return View(ItemCategory);
        }

        // POST: ItemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("1"))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.ItemCategoriesDelete(id, token);
            return RedirectToAction(nameof(Index));
        }

    }
}
