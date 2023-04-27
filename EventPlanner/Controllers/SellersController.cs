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
    [Authorize]
    public class SellersController : Controller
    {


        public SellersController() { }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            Seller seller = await Functions.APIServices.SellersDetails(id, token);
            return View(seller);
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> DetailsAdm(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            Seller seller = await Functions.APIServices.SellersDetails(id, token);
            return View(seller);
        }

        // GET: Sellers/Create
        public async Task<IActionResult> Create()
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            int? sellerId = await Functions.APIServices.SellerExists(token);
            if (sellerId != null && sellerId != 0)
            {
                return RedirectToAction("Details", new { id = sellerId });
            }
            IEnumerable<IdentificationType> identificationTypes = await Functions.APIServices.IdentificationTypesGetList(token);
            ViewData["IdentificationTypeId"] = new SelectList(identificationTypes, "Id", "Name");
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyName, IdentificationTypeId, IdentificationNumber, ExperienceYears, Freelance")] Seller Seller)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.SellersCreate(Seller, token);
            if (generalResult.Result)
            {
                int? sellerId = await Functions.APIServices.SellerExists(token);
                return RedirectToAction("Details", new {id = sellerId});
            }
            return View(Seller);
        }
        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            Seller Seller = await Functions.APIServices.SellersDetails(id, token);
            IEnumerable<IdentificationType> identificationTypes = await Functions.APIServices.IdentificationTypesGetList(token);
            ViewData["IdentificationTypeId"] = new SelectList(identificationTypes, "Id", "Name");
            return View(Seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,UserId, CompanyName, IdentificationTypeId, IdentificationNumber, ExperienceYears, Freelance")] Seller Seller)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.SellersEdit(Seller, token);
            if (generalResult.Result)
            {
                int? sellerId = await Functions.APIServices.SellerExists(token);
                return RedirectToAction("Details", new { id = sellerId });
            }
            return View(Seller);
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            Seller Seller = await Functions.APIServices.SellersDetails(id, token);

            return View(Seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.SellersDelete(id, token);
            return RedirectToAction(nameof(Index));
        }


    }
}
