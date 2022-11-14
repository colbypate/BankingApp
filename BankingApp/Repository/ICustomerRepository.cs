using BankingApp.Models;

namespace BankingApp.Repository
{
    public interface ICustomerRepository
    {
        public bool CustomerLogin(string login, string password);
        public Customer GetCustomer(string login, string password);

        public void CreateCustomer (Customer customerToCreate);
        public Customer AddCustomer (Customer customer);
        public void UpdateCustomer (Customer customer);


    }

}
