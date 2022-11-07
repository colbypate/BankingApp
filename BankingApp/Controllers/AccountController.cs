﻿using BankingApp.Models;
using BankingApp.Repository;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Plugins;

namespace BankingApp.Controllers
{
    public class AccountController : Controller
    {
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
            //_contextAccessor.HttpContext.Session.SetInt32("accountid", accountid);
            //var accID = _contextAccessor.HttpContext.Session.GetInt32("accountid");
            //var acc = repo.GetAccountByID(accID);

            //Console.WriteLine(accID);
            //Console.WriteLine(acc);
            var accID = _contextAccessor.HttpContext.Session.GetInt32("accountid");
            var acc = repo.GetAccountByID((int)accID);
            Console.WriteLine(acc.balance);
            return View(acc);

           
        }

        [HttpPost]
        public IActionResult Transaction(string transferType, int amount, int accountid)
        {
            if(transferType == "deposit")
            {
                repo.Deposit(amount, accountid);
                return View();
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

            _contextAccessor.HttpContext.Session.SetInt32("balance", acc.balance);

            return RedirectToAction("Transfer", "Account", new { @accountid = accID});
        }
    }
}
//return RedirectToAction("Dashboard", "Home", customer);     NEED to implement this to my transfer so that i can have the variable 