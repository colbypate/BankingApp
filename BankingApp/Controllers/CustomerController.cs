using BankingApp.Models;
using BankingApp.Repository;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using Org.BouncyCastle.Crypto.Tls;
using System.Drawing;

namespace BankingApp.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ICustomerRepository repo;
        private readonly IHttpContextAccessor _contextAccessor;
        public CustomerController(IHttpContextAccessor contextAccessor, ICustomerRepository repo)
        {
            _contextAccessor = contextAccessor;
            this.repo = repo;
        }


        public IActionResult Index ()
        {
            return View();  
        }



        [HttpGet]
        public IActionResult Login()
        {

            return View();

        }
        
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            _contextAccessor.HttpContext.Session.SetString("login", login);
            _contextAccessor.HttpContext.Session.SetString("password", password);

            string userLogin = _contextAccessor.HttpContext.Session.GetString("login");
            string userPassword = _contextAccessor.HttpContext.Session.GetString("password");


            var valid = repo.CustomerLogin(userLogin, userPassword);
            if (valid == false)
            {
                return View("FailedSignIn");
            }

            var customer = repo.GetCustomer(userLogin, userPassword);
            var id = repo.GetCustomer(userLogin, userPassword).userid;

            _contextAccessor.HttpContext.Session.SetInt32("userid", id);
            var userid = _contextAccessor.HttpContext.Session.GetInt32("userid");
            var test = repo.GetCustomer(userLogin, userPassword).ToJson();
            

            return RedirectToAction("Dashboard", "Home", customer);
        }

        public IActionResult Overview()
        {
            
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }
        public IActionResult Register(Customer customerToCreate)
        {
            var newCustomer = repo.AddCustomer(customerToCreate);
            return View(newCustomer);
        }
        public IActionResult RegisterCustomerToDatabase(Customer customerToCreate)
        {
            repo.CreateCustomer(customerToCreate);
            return RedirectToAction("Index");
        }
    }

}
