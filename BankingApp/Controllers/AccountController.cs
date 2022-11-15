using BankingApp.Models;
using BankingApp.Repository;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Plugins;

namespace BankingApp.Controllers
{
    public class AccountController : Controller
    {
        //connects to my Account Interface allowing us to call those methods and allows us to create session variables
        private readonly IAccountRepository repo;
        private readonly IHttpContextAccessor _contextAccessor;
        public AccountController(IHttpContextAccessor contextAccessor, IAccountRepository repo)
        {
            _contextAccessor = contextAccessor;
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult Transfer(int accountid)
        {

            var accID = _contextAccessor.HttpContext.Session.GetInt32("accountid");
            var acc = repo.GetAccountByID((int)accID);

            Console.WriteLine(acc.balance);
            return View(acc);

           
        }

        [HttpPost]
        public IActionResult Transaction(string transactionType, decimal transactionAmount)
        {
            Console.WriteLine($"this is the transferType {transactionType} ");

            if (transactionType == "deposit")
            {
                var accID = _contextAccessor.HttpContext.Session.GetInt32("accountid");
                repo.Deposit(transactionAmount, (int)accID);
                var acc = repo.GetAccountByID((int)accID);
                return View(acc);
            }
            if(transactionType == "withdraw")
            {
                var accID = _contextAccessor.HttpContext.Session.GetInt32("accountid");
                repo.Withdraw(transactionAmount, (int)accID);
                var acc = repo.GetAccountByID((int)accID);
                return View(acc);
            }
            return View();

        }
        public IActionResult ViewAccounts(int id)
        {

            Console.WriteLine($"This is the userid: {id}");
            var accounts = repo.GetAccount(id);
            return View(accounts);
        }
        public IActionResult ChosenAccount( int testing)
        {
            _contextAccessor.HttpContext.Session.SetInt32("accountid", testing);
            var accID = _contextAccessor.HttpContext.Session.GetInt32("accountid");
            var acc = repo.GetAccountByID((int)accID);

            Console.WriteLine(accID);
            Console.WriteLine(acc);

            _contextAccessor.HttpContext.Session.SetString("balance", acc.balance.ToString());

            return RedirectToAction("Transfer", "Account", new { @accountid = accID});
        }

        public IActionResult CreateAccount()
        {

            return View();
        }
        public IActionResult CreateAccountToDatabase(Account account)
        {
            var newAcc = repo.CreateAccount(account);
            var id = newAcc.userid;
            return RedirectToAction("ViewAccounts", "Account", new { id = id });
        }
    }
}