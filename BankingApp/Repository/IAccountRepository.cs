using BankingApp.Models;

namespace BankingApp.Repository
{
    public interface IAccountRepository
    {
        public IEnumerable<Account> GetAccount(int userid);
        public void Deposit(int deposit, int accountid);
        public int Withdraw(int withdraw, int accountid);
        public Account CreateAccount(Account account);
        public Account GetAccountByID(int accountid);  


    }
}
