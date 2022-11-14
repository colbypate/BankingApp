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

    }
}
