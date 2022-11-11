using BankingApp.Controllers;
using BankingApp.Models;
using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BankingApp.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _conn;
        public CustomerRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public void CreateCustomer(Customer customerToCreate)
        {
            _conn.Execute("insert into customer (name, phonenumber, emailaddress, login, password) values (@name, @phonenumber, @emailaddress, @login, @password);",
                new { name = customerToCreate.name, phonenumber = customerToCreate.phonenumber, emailaddress = customerToCreate.emailaddress, login = customerToCreate.login, password = customerToCreate.password });
        }
        public Customer AddCustomer(Customer customer)
        {
            var newCustomer = new Customer();
            return newCustomer;
        }

        public bool CustomerLogin(string login, string password)
        {
            var valid = _conn.ExecuteScalar<bool>("select COUNT(DISTINCT 1) from customer where login = @login and password = @password",
                    new { login = login, password = password });
            if (valid == false)
            {
                return false;
            }
            return true;
        }



        public Customer GetCustomer(string login, string password)
        {
            var customer = new Customer();
            customer = _conn.QuerySingle<Customer>("select * from customer where login = @login and password = @password;",
                    new { login, password });
            return customer;
        }


        public void UpdateCustomer(Customer newCustomer)
        {
            
            _conn.Execute("update customer set name = @name, phonenumber = @phonenumber, emailaddress = @emailaddress, login = @login, password = @password where userid = @id;",
                new { name = newCustomer.name, phonenumber = newCustomer.phonenumber, emailaddress = newCustomer.emailaddress, login = newCustomer.login, password = newCustomer.password, id = newCustomer.userid });
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
            return newAcc;
        }
    }
}
