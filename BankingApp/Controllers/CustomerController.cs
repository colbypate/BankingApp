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

        public IActionResult Dashboard(Customer customer)
        {
            _contextAccessor.HttpContext.Session.SetInt32("userid", customer.userid);
            var userLogin = _contextAccessor.HttpContext.Session.GetString("login");
            var userPassword = _contextAccessor.HttpContext.Session.GetString("password");
            Console.WriteLine($"This is the login {userLogin}");
            Console.WriteLine($"This is the password {userPassword}");

            if (customer == null)
            {
                Console.WriteLine($"This is the login 2 {userLogin}");
                Console.WriteLine($"This is the password 2 {userPassword}");
                var cust = new Customer();
                cust = repo.GetCustomer(userLogin, userPassword);
                return View(cust);
            }


            return View(customer);
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
            

            return RedirectToAction("Dashboard", "Customer", customer);
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
            TempData["AlertMessage"] = "Customer created successfully... Login to view your accounts.";
            return RedirectToAction("Login");
        }

        public IActionResult UpdateCustomer(Customer customerToUpdate)
        {
            string userLogin = _contextAccessor.HttpContext.Session.GetString("login");
            string userPassword = _contextAccessor.HttpContext.Session.GetString("password");
            var customer = repo.GetCustomer(userLogin, userPassword);
            repo.UpdateCustomer(customer);
            return View(customer);
        }

        public IActionResult UpdateCustomerToDatabase(Customer customerToUpdate)
        {
            
            repo.UpdateCustomer(customerToUpdate);
            TempData["Alert"] = " Info updated successfully... ";
            return RedirectToAction("Dashboard", "Customer", customerToUpdate);
        }
    }

}
