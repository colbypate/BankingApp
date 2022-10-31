using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class Login
    {
        public string login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
