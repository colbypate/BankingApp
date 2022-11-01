using BankingApp.Models;
using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace BankingApp.Repository
{
    public class CustomerRepository : ICustomerRepository, IAccountRepository
    {
        private readonly IDbConnection _conn;
        public CustomerRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public Customer ChooseAccountType()
        {
            throw new NotImplementedException();
        }

        public Account CreateAccount(Account account)
        {
            throw new NotImplementedException();
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

        public int Deposit(int deposit)
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(int userid)
        {
            return _conn.QuerySingle<Account>("select * from account where userid = @userid",
                new {userid = userid});
        }

        public Customer GetCustomer(string login, string password)
        {
            return _conn.QuerySingle<Customer>("select * from customer where login = @login and password = @password",
                    new { login = login, password = password });

        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public int Withdraw(int withdraw)
        {
            throw new NotImplementedException();
        }
    }
}
