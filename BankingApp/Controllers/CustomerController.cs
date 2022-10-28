using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [Route("_customers")]
    public class CustomerController : Controller
    {
        private ICustomerLogin customerLogin;
        public CustomerController(ICustomerLogin _customerLogin)
        {
            this.customerLogin = _customerLogin;
        }

        [Route("")]
        [Route("~/")]
        [Route("index")]

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string login, string password)
        {
            var customer = customerLogin.Login(login, password);
            if (customer != null)
            {
                HttpContext.Session.SetString("login", login);
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewBag.msg = "Invalid";
                return View("Index");
            }
        }
        [Route("welcome")]
        public IActionResult Welcome()
        {
            ViewBag.login = HttpContext.Session.GetString("login");
            return View("Welcome");
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("login");
            return RedirectToAction("index");
        }
    }

}
