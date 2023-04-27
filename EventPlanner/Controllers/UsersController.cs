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
    
    public class UsersController : Controller
    {
        

        public UsersController() { }


        // GET: Users
        [Authorize(Roles =("1"))]
        public async Task<IActionResult> Index()
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            IEnumerable<User> users = await Functions.APIServices.UsersGetList(token);

            return View(users);
        }

        // GET: Users/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            User user = await Functions.APIServices.UsersDetails(id, token);
            ViewBag.SellerId = await Functions.APIServices.SellerExists(token);
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            User user = await Functions.APIServices.UsersDetails(id, token);
            IEnumerable<Role> roles = await Functions.APIServices.RolesGetList(token);
            ViewData["RoleId"] = roles.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.RoleName,
                Selected = (s.Id == user.RoleId)
            }).ToList();
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int Id, [Bind("FirstName,LastName,CompanyName,MailVisible,Email,PhoneVisible,ContactPhone,IsCompany, RoleId")] User user)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.UsersEdit(Id, user, token);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = ("1"))]
        public async Task<IActionResult> Delete(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            User user = await Functions.APIServices.UsersDetails(id, token);
            
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("1"))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            GeneralResult generalResult = await Functions.APIServices.UsersDelete(id, token);
            return RedirectToAction(nameof(Index));
        }

    }
}
