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
    
    public class VerificationRequestsController : Controller
    {


        public VerificationRequestsController() { }


        // GET: VerificationRequests
        [Authorize(Roles =("1"))]
        public async Task<IActionResult> Index()
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            IEnumerable<VerificationRequest> VerificationRequests = await Functions.APIServices.VerificationRequestsGetList(token);

            return View(VerificationRequests);
        }

        // GET: VerificationRequests/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            VerificationRequest VerificationRequest = await Functions.APIServices.VerificationRequestsDetails(id, token);
            return View(VerificationRequest);
        }

        // GET: VerificationRequests/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            ViewBag.SellerId = await Functions.APIServices.SellerExists(token);
            return View();
        }

        // POST: VerificationRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,SellerId,AdminId,Description,AdminComments,StatusId")] VerificationRequest VerificationRequest)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.VerificationRequestsCreate(VerificationRequest, token);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(VerificationRequest);
        }
        // GET: VerificationRequests/Edit/5
        [Authorize(Roles =("1"))]
        public async Task<IActionResult> Edit(int? id)
        {

            string token = User.Claims.First(s => s.Type == "token").Value;
            VerificationRequest VerificationRequest = await Functions.APIServices.VerificationRequestsDetails(id, token);
            IEnumerable<VerificationStatus> status = await Functions.APIServices.VerificationStatusesGetList(token);
            ViewData["StatusId"] = new SelectList(status, "Id", "Name");
            ViewBag.CanEdit = true;
            if (VerificationRequest.StatusId != 1)
            {
                
                ViewBag.CanEdit = false;
            }
            return View(VerificationRequest);
        }

        // POST: VerificationRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("1"))]
        public async Task<IActionResult> Edit([Bind("Id,SellerId,AdminId,Description,AdminComments,StatusId")] VerificationRequest VerificationRequest)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.VerificationRequestsEdit(VerificationRequest, token);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(VerificationRequest);
        }

        // GET: VerificationRequests/Delete/5
        [Authorize(Roles = ("1"))]
        public async Task<IActionResult> Delete(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            VerificationRequest VerificationRequest = await Functions.APIServices.VerificationRequestsDetails(id, token);

            return View(VerificationRequest);
        }

        // POST: VerificationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("1"))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.VerificationRequestsDelete(id, token);
            return RedirectToAction(nameof(Index));
        }

    }
}
