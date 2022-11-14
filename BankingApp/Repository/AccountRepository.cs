using BankingApp.Models;
using BankingApp.Models.ViewModels;
using Dapper;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BankingApp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnection _conn;
        public AccountRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public Account GetAccountByID (int accountid)
        {
            var account = new Account();
            account = _conn.QuerySingle<Account>("SELECT * FROM account where accountid = @accountid;",
                new { accountid = accountid });
            
            return account;
        }

        public void Deposit(decimal amount, int testing)
        {
            _conn.Execute("UPDATE account SET balance = balance + @amount WHERE accountid = @accountid;",
                new { amount = amount, accountid = testing });
        }

        public IEnumerable<Account> GetAccount(int userid)
        {
           
            //IF NOT WORK REPLACE WITH : SELECT * FROM customer inner JOIN account ON customer.userid = @userid and account.userid = @userid where login = @login and password = @password
            var account = _conn.Query<Account>("SELECT * FROM account where userid = @userid;",
                    new { userid = userid });
            return account;
        }

        public void Withdraw(decimal amount, int testing)
        {
            _conn.Execute("UPDATE account SET balance = balance - @amount WHERE accountid = @accountid;",
                new { amount = amount, accountid = testing });
        }
        public Account CreateAccount(Account account)
        {
            var accountToCreate = new Account();
            _conn.Execute("insert into account (userid, accounttype, balance, accountid) values (@userid, @accounttype, @balance, @accountid);",
                new { userid = account.userid, accounttype = account.accounttype, balance = accountToCreate.balance, accountid = 0 });
            Random rnd = new Random();

            var notAvailable = _conn.Query("select accountid from account where userid = @userid;",
                new { userid = account.userid });
            string num = rnd.Next(0, 98).ToString();
            var n = account.userid.ToString() + num;
            int accID = int.Parse(n);
            var tryNum = _conn.Query("select accountid from account where accountid = @accountid;",
                new { accountid = accID });
            if (notAvailable == tryNum)
            {
                accID = accID + 1;
            }
            _conn.Execute("update account set accountid = @accountid where accountid = '0';",
                new { accountid = accID });
            var newAcc = _conn.QuerySingle<Account>("select * from account where @accountid = accountid;",
                    new { accountid = accID });
            accID = accID + 1;
            return newAcc;
        }

    }
}
