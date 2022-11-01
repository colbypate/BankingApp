using BankingApp.Models;
using BankingApp.Repository;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Drawing;

namespace BankingApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository repo;
        public CustomerController(ICustomerRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index ()
        {
            return View();  
        }
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            var valid = repo.CustomerLogin(login, password);
            if (valid == false)
            {
                return View("FailedSignIn");
            }
            ViewBag.customer = repo.GetCustomer(login, password);
            return View(ViewBag.customer);
        }
        public IActionResult Logout()
        {
            return View();
        }
        public IActionResult Register(Customer customerToCreate)
        {
            var customer = repo.AddCustomer(customerToCreate);
            return View(customer);
        }
        public IActionResult RegisterCustomerToDatabase(Customer customerToCreate)
        {
            repo.CreateCustomer(customerToCreate);
            return RedirectToAction("Index");
        }
    }

}
