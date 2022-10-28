namespace BankingApp.Models
{
    public class Transaction
    {
        public int transactionid { get; set; }
        public int accountid { get; set; }
        public string type { get; set; }
        public int amount { get; set; }
        public DateTime date { get; set; }

    }
}
