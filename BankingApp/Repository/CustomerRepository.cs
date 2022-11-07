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
            ////IF NOT WORK REPLACE WITH : SELECT * FROM customer inner JOIN account ON customer.userid = @userid and account.userid = @userid where login = @login and password = @password
            //var account = _conn.Query<Customer>("SELECT * FROM customer inner JOIN account ON customer.userid = @userid and account.userid = @userid;",
            //        new { userid = custID.userid});
            //return account;

        }

        //public Account GetAccount(Customer customer)
        //{
        //    Console.WriteLine(customer.userid);
        //    Console.WriteLine(customer.name);
        //   // var id = _conn.QuerySingle<Customer>("select userid from customer where login = @login and password = @password",
        //     //   new { login = customer.login, password = customer.password });

        //    var account = _conn.QuerySingle<Account>("select userid from customer where userid = @userid",
        //        new { userid = customer.userid });
        //    return account;
        //}

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }


    }
}
