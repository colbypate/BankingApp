using BankingApp.Models;

namespace BankingApp.Repository
{
    public interface ICustomerRepository
    {
        public Customer GetCustomer(string login, string password);
        public void CreateCustomer (Customer customerToCreate);
        public void UpdateCustomer (Customer customer);
        public Customer ChooseAccountType();
    }

}
