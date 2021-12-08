using System.ComponentModel.DataAnnotations;

namespace Auction.DAL.Models
{
    public class AccountType
    {
        [Key]
        public int AccountTypeId { get; set; }
        [Required]
        public string AccountTypeName { get; set; }
    }
}
