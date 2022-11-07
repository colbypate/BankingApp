using BankingApp.Models;
using BankingApp.Repository;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BankingApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository repo;
        public AccountController(IAccountRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Deposit()
        {
            
            return View();
        }
        public IActionResult ViewAccounts(int id)
        {
            Console.WriteLine($"This is the userid: {id}");
            var accounts = repo.GetAccount(id);
            return View(accounts);
        }
    }
}
