using BankingApp.Models;

namespace BankingApp
{
    public class CustomerLoginImpl : ICustomerLogin
    {

        private List<Customer> _customers;
        public CustomerLoginImpl()
        {
            _customers = new List<Customer>
            {
                new Customer
                {
                    login = "acc1"
                }
            };
        }
        public Customer Login(string login, string password)
        {
            return _customers.SingleOrDefault(a => a.login == login && a.password == password);
        }
    }
}
