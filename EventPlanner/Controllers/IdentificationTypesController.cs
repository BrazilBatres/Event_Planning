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
    public class IdentificationTypesController : Controller
    {


        public IdentificationTypesController() { }


        // GET: IdentificationTypes
        public async Task<IActionResult> Index()
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            IEnumerable<IdentificationType> IdentificationTypes = await Functions.APIServices.IdentificationTypesGetList(token);

            return View(IdentificationTypes);
        }

        // GET: IdentificationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            IdentificationType IdentificationType = await Functions.APIServices.IdentificationTypesDetails(id, token);
            return View(IdentificationType);
        }

        // GET: IdentificationTypes/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: IdentificationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] IdentificationType IdentificationType)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.IdentificationTypesCreate(IdentificationType, token);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(IdentificationType);
        }
        // GET: IdentificationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            IdentificationType IdentificationType = await Functions.APIServices.IdentificationTypesDetails(id, token);
            return View(IdentificationType);
        }

        // POST: IdentificationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name")] IdentificationType IdentificationType)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.IdentificationTypesEdit(IdentificationType, token);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(IdentificationType);
        }

        // GET: IdentificationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            IdentificationType IdentificationType = await Functions.APIServices.IdentificationTypesDetails(id, token);

            return View(IdentificationType);
        }

        // POST: IdentificationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.IdentificationTypesDelete(id, token);
            return RedirectToAction(nameof(Index));
        }

    }
}
