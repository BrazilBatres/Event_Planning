using EventPlanner.Models;
using EventPlannerModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace EventPlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            string token = User.Claims.First(s => s.Type == "token").Value;
            string roleid = User.Claims.First(s => s.Type == ClaimTypes.Role).Value;
            EventPlannerModels.Role role = await Functions.APIServices.RolesDetails(int.Parse(roleid),token);
            ViewBag.Role = role.RoleName;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}