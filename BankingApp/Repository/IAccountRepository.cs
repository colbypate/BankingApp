using BankingApp.Models;

namespace BankingApp.Repository
{
    public interface IAccountRepository
    {
        public Account GetAccount(int userid);
        public int Deposit(int deposit);
        public int Withdraw(int withdraw);
        public Account CreateAccount(Account account);


    }
}
