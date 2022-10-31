using BankingApp.Models;
using Dapper;
using System.Data;

namespace BankingApp.Repository
{
    public class CustomerRepository : ICustomerRepository
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

        public void CreateCustomer(Customer customerToCreate)
        {
            _conn.Execute("insert into customer (name, phonenumber, emailaddress, login, password) values (@name, @phonenumber, @emailaddress, @login, @password);",
                new { name = customerToCreate.name, phonenumber = customerToCreate.phonenumber, emailaddress = customerToCreate.emailaddress, login = customerToCreate.login, password = customerToCreate.password });
        }

        public Customer GetCustomer(string login, string password)
        {
            return _conn.QuerySingle<Customer>("select * from customer where login = @login and password = @password");
        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
