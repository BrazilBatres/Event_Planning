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
    public class ReferralsController : Controller
    {


        public ReferralsController() { }


        // GET: Referrals
        public async Task<IActionResult> Index()
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            IEnumerable<Referral> Referrals = await Functions.APIServices.ReferralsGetList(token);

            return View(Referrals);
        }

        // GET: Referrals
        public async Task<IActionResult> IndexByUser(int sellerid)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            IEnumerable<Referral> Referrals = await Functions.APIServices.ReferralsGetListById(token, sellerid);

            return View("IndexAdm",Referrals);
        }

        // GET: Referrals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            Referral Referral = await Functions.APIServices.ReferralsDetails(id, token);
            return View(Referral);
        }

        // GET: Referrals/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Referrals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SellerId,Name,Phone,Email")] Referral Referral)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.ReferralsCreate(Referral, token);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(Referral);
        }
        // GET: Referrals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            Referral Referral = await Functions.APIServices.ReferralsDetails(id, token);
            return View(Referral);
        }

        // POST: Referrals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,SellerId,Name,Phone,Email")] Referral Referral)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.ReferralsEdit(Referral, token);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(Referral);
        }

        // GET: Referrals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            Referral Referral = await Functions.APIServices.ReferralsDetails(id, token);

            return View(Referral);
        }

        // POST: Referrals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.ReferralsDelete(id, token);
            return RedirectToAction(nameof(Index));
        }

    }
}
