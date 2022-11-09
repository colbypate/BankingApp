using BankingApp.Models;
using BankingApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Permissions;

namespace BankingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult LoadAccounts()
        {
            var id = _contextAccessor.HttpContext.Session.GetInt32("userid");
            Console.WriteLine($"This is the first id: {id}");
            return RedirectToAction("ViewAccounts", "Account", new {@id = id});

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}