using BankingApp.Models;
using Dapper;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Drawing;

namespace BankingApp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnection _conn;
        public AccountRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public void GetUserID ()
        {
            
        }
        public Account CreateAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public void Deposit(int deposit)
        {
        //    var dep = _conn.QuerySingle<Account>("UPDATE account SET balance = balance + deposit WHERE accountid = @accountid;"
        //        new {deposit = deposit, accountid = IDKKKKKK});
        }

        public IEnumerable<Account> GetAccount(int userid)
        {
            //IF NOT WORK REPLACE WITH : SELECT * FROM customer inner JOIN account ON customer.userid = @userid and account.userid = @userid where login = @login and password = @password
            var account = _conn.Query<Account>("SELECT * FROM account where userid = @userid;",
                    new { userid = userid });
            return account;
        }

        public int Withdraw(int withdraw)
        {
            throw new NotImplementedException();
        }

    }
}
