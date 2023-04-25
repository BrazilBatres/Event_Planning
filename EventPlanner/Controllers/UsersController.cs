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

        [Authorize]
        // GET: Users
        public async Task<IActionResult> Index()
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            IEnumerable<User> users = await Functions.APIServices.UsersGetList(token);

            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            User user = await Functions.APIServices.UsersDetails(id, token);
            return View(user);
        }
        // GET: Users/Login
        public async Task<IActionResult> Login()
        {
            return View();
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login([Bind("Email,Password")] LoginUser user)
        //{
        //    GeneralResult generalResult = await Functions.APIServices.UsersLogin(user);
        //    if (generalResult.Result)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    TempData["errorMsg"] = generalResult.ErrorMessage;
        //    return View(user);
        //}

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,CompanyName,MailVisible,Email,PhoneVisible,ContactPhone,IsCompany,Password")] User user)
        {
            GeneralResult generalResult = await Functions.APIServices.UsersCreate(user);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            User user = await Functions.APIServices.UsersDetails(id, token);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("FirstName,LastName,CompanyName,MailVisible,Email,PhoneVisible,ContactPhone,IsCompany")] User user)
        {
            GeneralResult generalResult = await Functions.APIServices.UsersEdit(Id, user);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            User user = await Functions.APIServices.UsersDetails(id, token);
          
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            GeneralResult generalResult = await Functions.APIServices.UsersDelete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
