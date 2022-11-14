using BankingApp.Models;

namespace BankingApp.Repository
{
    public interface IAccountRepository
    {
        public IEnumerable<Account> GetAccount(int userid);
        public void Deposit(decimal deposit, int accountid);
        public void Withdraw(decimal withdraw, int accountid);
        public Account GetAccountByID(int accountid);
        public Account CreateAccount(Account account);


    }
}
