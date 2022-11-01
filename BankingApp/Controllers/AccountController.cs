using BankingApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository repo;
        public AccountController(IAccountRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Overview(int userid)
        {
            ViewBag.account = repo.GetAccount(userid);
            return View();
        }
    }
}
