using BankingApp.Models;

namespace BankingApp.Repository
{
    public interface IAccountRepository
    {
        public IEnumerable<Account> GetAccount(int userid);
        public void Deposit(int deposit);
        public int Withdraw(int withdraw);
        public Account CreateAccount(Account account);


    }
}
